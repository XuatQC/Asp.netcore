using BCrypt.Net;
using FNDSystem.Core.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Office.Interop.Word;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FNDSystem.Core.Helpers;
public static class StringUtils
{
    internal static AuthTokenBase JwtAuthTokenBase = new AuthTokenBase();

    public static void SetAuthTokenBase(AuthTokenBase authTokenBase)
    {
        JwtAuthTokenBase = authTokenBase;
        if (string.IsNullOrEmpty(JwtAuthTokenBase?.SecretKey))
        {
            string _defaultSecretKey = "6ceccd7405ef4b00b2630009be568cfa";
            if (JwtAuthTokenBase != null) JwtAuthTokenBase.SecretKey = _defaultSecretKey;
        }
    }

    public static byte[] GenerateSecretByte() =>
        Encoding.UTF8.GetBytes(JwtAuthTokenBase.SecretKey);
    public static string GenerateJWTToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = GenerateSecretByte();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Initial", user.Initial),
                new Claim("name", user.Name)
            }),
            Expires = DateTime.UtcNow.AddMinutes(120),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public static JwtSecurityToken? VerifyJWTToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = GenerateSecretByte();
            var hmacsha256 = new HMACSHA256(key);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(hmacsha256.Key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return jwtToken;
        }
        catch
        {
            return null;
        }
    }

    public static string PasswordHash(string rawPassword)
    {
        string hashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(rawPassword, HashType.SHA512);
        return hashPassword;
    }

    public static bool VerifyPasswordHash(string rawPassword, string hashPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(rawPassword, hashPassword, HashType.SHA512);
    }

    /// <summary>
    /// ドキュメント読み込みより内容を取得する
    /// </summary>
    /// <param name="filePath">ファイルパス</param>
    /// <returns>ファイルの内容</returns>
    public static StringBuilder GetWordFileContent(string filePath)
    {
        Application wordApp = new Application();

        //creating the object of document class  
        Document doc;

        //get the uploaded file full path  
        dynamic FilePath = Path.GetFullPath(filePath);

        //pass the optional (missing) parameter to API  
        dynamic NA = Type.Missing;

        //open Word file document   
        doc = wordApp.Documents.Open
                      (ref FilePath, ref NA, ref NA, ref NA, ref NA,
                       ref NA, ref NA, ref NA, ref NA,
                       ref NA, ref NA, ref NA, ref NA,
                       ref NA, ref NA, ref NA);

        //creating the object of string builder class  
        StringBuilder fileContent = new StringBuilder();

        for (int Line = 0; Line < doc.Paragraphs.Count; Line++)
        {
            string Filedata = doc.Paragraphs[Line + 1].Range.Text.Trim();

            if (Filedata != string.Empty)
            {
                //Append word files data to stringbuilder  
                fileContent.AppendLine(Filedata);
            }
        }

        //closing document object   
        doc.Close();

        //Quit application object to end process  
        wordApp.Quit();
        return fileContent;
    }

    public static string? PastTransSentence(Application wd, int pRow, int pCol)
    {
        string? result = string.Empty;

        if (!wd.Visible) wd.Visible = true;


        //' セル選択
        wd.ActiveDocument.Tables[1].Cell(pRow, pCol).Select();

        //' セル選択
        wd.ActiveDocument.Tables[1].Cell(pRow, pCol).Select();

        //' フォントサイズ 12P
        wd.Selection.Font.Size = 12;

        //' カーソルを1つ戻す

        //' 文書をコピー

        result = wd.ActiveDocument?.Tables[1].Cell(pRow, pCol).ToString();
        return result;
    }

    /// <summary>
    /// g_予約文字の変換処理
    /// reserved character conversion processing
    /// </summary>
    /// <param name="pData">Data</param>
    /// <returns>convert Data</returns>
    public static string ResCharConPro(string pData)
    {
        string cnsBeforeConvert = "@";
        string cnsAfterConvert = "＠";

        return pData.Replace(cnsBeforeConvert, cnsAfterConvert);
    }

    public static bool ToBoolean(this string value)
    {
        return !string.IsNullOrEmpty(value) && value != "0" && value != "false";
    }

    /// <summary>
    /// Genareate num by number control
    /// </summary>
    /// <param name="obsExtend"></param>
    /// <returns></returns>
    public static string GenerateNum(NumberControl obsExtend)
    {
        string num = $"{obsExtend.PlantName}-{obsExtend.Kinds}-{obsExtend.Fields}-{obsExtend.PartId}";
        num += $"-{obsExtend.SerialNum.ToString("00")}-r{obsExtend.Revisions}";
        if (obsExtend.Language == LanguageCode.English) num += "-E";
        return num;
    }
}