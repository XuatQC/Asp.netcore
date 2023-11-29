using FNDSystem.Core.ServicesContracts;
using Microsoft.Office.Interop.Word;
using System.Text;

namespace FNDSystem.Core.Services;
public class DocumentsService : GeneticService, IDocumentsService
{
    public DocumentsService()
    {
    }

    /// <summary>
    /// ドキュメント読み込みより内容を取得する
    /// </summary>
    /// <param name="filePath">ファイルパス</param>
    /// <returns>ファイルの内容</returns>
    public StringBuilder GetWordFileContent(string filePath)
    {

        Application wordApp = new Application();

        //ドキュメントクラスのオブジェクトを作成する
        Document doc;

        //アップロードされたファイルのフルパスを取得します
        dynamic FilePath = Path.GetFullPath(filePath);

        //オプションの（欠落している）パラメータを API に渡します
        dynamic NA = System.Type.Missing;

        //Wordファイル文書を開く
        doc = wordApp.Documents.Open
                      (ref FilePath, ref NA, ref NA, ref NA, ref NA,
                       ref NA, ref NA, ref NA, ref NA,
                       ref NA, ref NA, ref NA, ref NA,
                       ref NA, ref NA, ref NA);

        //文字列ビルダークラスのオブジェクトを作成する
        StringBuilder fileContent = new StringBuilder();

        for (int Line = 0; Line < doc.Paragraphs.Count; Line++)
        {
            string Filedata = doc.Paragraphs[Line + 1].Range.Text.Trim();

            if (Filedata != string.Empty)
            {
                //Word ファイルのデータを stringbuilder に追加する
                fileContent.AppendLine(Filedata);
            }
        }

        //ドキュメントオブジェクトを閉じる
        ((_Document)doc).Close();

        //アプリケーションオブジェクトを終了してプロセスを終了します
        ((_Application)wordApp).Quit();

        return fileContent;
    }
}