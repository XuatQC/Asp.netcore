using FOFB.Shared.Entities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Utility
{
	public class Common
	{
		private readonly SettingsConfig _config;
		public Common(SettingsConfig config)
		{
			_config = config;
		}

		#region 管理画面ログインにIP制限をチェックする
		/// <summary>
		/// 管理画面ログインにIP制限をチェックする
		/// </summary>
		/// <param name="currNetworkIp"></param>
		/// <returns></returns>
		public bool IsValidIp(string currNetworkIp)
		{
			string[] listConfigIp = _config.AdminAccessIP.Split(",").Select(t => t.Trim()).ToArray();

			int isValidIp = Array.IndexOf(listConfigIp, currNetworkIp);

			return (isValidIp > -1);
		}
		#endregion

		#region Get current Japan datetime
		/// <summary>
		/// Get current Japan datetime
		/// </summary>
		/// <returns></returns>
		public DateTime GetCurrJPDateTime()
		{
			DateTime utc = DateTime.UtcNow;
			TimeZoneInfo tokyoZone;
			try
			{
				// window
				tokyoZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
			}
			catch (TimeZoneNotFoundException)
			{
				// unix
				tokyoZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");
			}
			DateTime now = TimeZoneInfo.ConvertTimeFromUtc(utc, tokyoZone);

			return now;
		}
		#endregion

		#region Compare data of 2 object
		/// <summary>
		/// Compare data of 2 object
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="another"></param>
		/// <returns></returns>
		public static bool ObjDataCompare(object obj, object another)
		{
			if (ReferenceEquals(obj, another)) return true;
			if ((obj == null) || (another == null)) return false;
			if (obj.GetType() != another.GetType()) return false;

			var objJson = JsonConvert.SerializeObject(obj);
			var anotherJson = JsonConvert.SerializeObject(another);

			return objJson == anotherJson;
		}
		#endregion

		#region Encript Password
		/// <summary>
		/// Encript Password
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string MD5Hash(string text)
		{
			MD5 md5 = new MD5CryptoServiceProvider();

			//compute hash from the bytes of text  
			md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

			//get hash result after compute it  
			byte[] result = md5.Hash;

			StringBuilder strBuilder = new StringBuilder();
			for (int i = 0; i < result.Length; i++)
			{
				//change it into 2 hexadecimal digits  
				//for each byte  
				strBuilder.Append(result[i].ToString("x2"));
			}

			return strBuilder.ToString();
		}
		#endregion

		#region Generate JSONWebToken(Jwt)
		/// <summary>
		/// GenerateJSONWebToken
		/// </summary>
		/// <param name="muserLogin"></param>
		/// <param name="isAutolLogin"></param>
		/// <returns></returns>
		public string GenerateJSONWebToken(MuserLoginSub muserLoginSub, bool isAutolLogin)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Jwt.Key));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.NameId, muserLoginSub.UserCd),
				new Claim("department", muserLoginSub.Department.ToString()), // 1: admin  2: shop
				new Claim("Permissions", muserLoginSub.Permissions.ToString()) // VIEW, UPD, DOWNLOAD_CSV, SHOP
			};

			JwtSecurityToken token;

			// If checked on auto login 
			if (isAutolLogin)
			{
				token = new JwtSecurityToken(
								issuer: _config.Jwt.Issuer,
								audience: _config.Jwt.Issuer,
								claims,
								expires: DateTime.UtcNow.AddDays(30),
								signingCredentials: credentials);
			}
			else
			{
				token = new JwtSecurityToken(
								issuer: _config.Jwt.Issuer,
								audience: _config.Jwt.Issuer,
								claims,
								expires: DateTime.UtcNow.AddMinutes(60),
								signingCredentials: credentials);
			}

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		#endregion

		#region orderIdリストを取得する
		/// <summary>
		/// orderIdリストを取得する
		/// </summary>
		/// <param name="ordersSubs"></param>
		/// <returns></returns>
		public string GetListOrderId(List<OrdersSub> ordersSubs, string mark = null)
		{
			string lstOrderId = string.Empty;
			for (int i = 0; i < ordersSubs.Count; i++)
			{
				lstOrderId += mark + ordersSubs[i].OrderId + mark;
				if (i < ordersSubs.Count - 1)
				{
					lstOrderId += ",";
				}
			}

			return lstOrderId;
		}
		#endregion

		#region csvファイルデータを読み込む
		/// <summary>
		/// csvファイルデータを読み込む
		/// </summary>
		/// <param name="path"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public List<List<string>> ReadCsv(IFormFile path,
									string encoding)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			List<List<string>> listA = new List<List<string>>();
			try
			{
				using (var reader = new StreamReader(path.OpenReadStream(), Encoding.GetEncoding(encoding)))
				{
					while (!reader.EndOfStream)
					{
						string line = reader.ReadLine();
						if(line == "")
						{
							continue;
						}
						line = line.Replace("\"", string.Empty);
						var values = line.Split(',').ToList();

						listA.Add(values);
					}
				}
				// Remove header
				listA.RemoveAt(0);
			}
			catch (Exception ex)
			{
				Log.Logger.Error("ReadCsv error: " + ex.Message);
			}
			return listA;
		}
		#endregion

		#region 受取人情報マッピング
		/// <summary>
		/// 受取人情報マッピング
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <returns></returns>
		public MCustrecieveSub ConvertObjectMCustrecieve(OrdersSub ordersSub)
		{
			MCustrecieveSub mCustrecieveSub = new MCustrecieveSub();
			mCustrecieveSub.KanjiFamilyName = ordersSub.KanjiFamilyNameMCustReceive;
			mCustrecieveSub.KanjiFirstName = ordersSub.KanjiFirstNameMCustReceive;
			mCustrecieveSub.KanaFamilyName = ordersSub.KanaFamilyNameMCustReceive;
			mCustrecieveSub.KanaFirstName = ordersSub.KanaFirstNameMCustReceive;
			mCustrecieveSub.ZipCodeDsp = ordersSub.ZipCodeDspMCustReceive;
			mCustrecieveSub.ProvinceName = ordersSub.ProvinceNameMCustReceive;
			mCustrecieveSub.AdrCd1 = ordersSub.AdrCd1MCustReceive;
			mCustrecieveSub.AdrCd2 = ordersSub.AdrCd2MCustReceive;
			mCustrecieveSub.AdrCd3 = ordersSub.AdrCd3MCustReceive;
			mCustrecieveSub.PhoneNumber = ordersSub.PhoneNumberMCustReceive;

			return mCustrecieveSub;
		}
		#endregion

		#region お客様情報マッピング
		/// <summary>
		/// お客様情報マッピング
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <returns></returns>
		public MCustSub ConvertObjectMCust(OrdersSub ordersSub)
		{
			MCustSub mCustSub = new MCustSub();
			mCustSub.KanjiFamilyName = ordersSub.KanjiFamilyName;
			mCustSub.KanjiFirstName = ordersSub.KanjiFirstName;
			mCustSub.KanaFamilyName = ordersSub.KanaFamilyName;
			mCustSub.KanaFirstName = ordersSub.KanaFirstName;
			mCustSub.ZipCodeDsp = ordersSub.ZipCodeDsp;
			mCustSub.ProvinceName = ordersSub.ProvinceName;
			mCustSub.AdrCd1 = ordersSub.AdrCd1;
			mCustSub.AdrCd2 = ordersSub.AdrCd2;
			mCustSub.AdrCd3 = ordersSub.AdrCd3;
			mCustSub.PhoneNumber = ordersSub.PhoneNumber;
			mCustSub.MailAddress = ordersSub.MailAddress;

			return mCustSub;
		}
		#endregion

		#region メール作成
		/// <summary>
		/// メール作成
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="orderDetails"></param>
		/// <param name="mCustSub"></param>
		/// <param name="mCustrecieveSub"></param>
		/// <param name="mailTemplate"></param>
		/// <param name="updateUserCd"></param>
		/// <param name="mBank"></param>
		/// <returns></returns>
		public SendMail CreateMail(OrdersSub ordersSub,
									List<OrderDetail> orderDetails,
									MCustSub mCustSub,
									MCustrecieveSub mCustrecieveSub,
									MailTemplate mailTemplate,
									string updateUserCd = null,
									MBank mBank = null)
		{
			string contentMail = CreateContentMail(ordersSub, mCustSub, mCustrecieveSub, orderDetails, mailTemplate.Content, mBank);

			SendMail mailCreate = new SendMail();
			mailCreate.OrderId = ordersSub.OrderId;
			mailCreate.MailTemplateId = mailTemplate.MailTemplateId;
			mailCreate.MailTitle = mailTemplate.Title.Replace(ParamConst.ItemMailTemplate.MAIL_RSV_ID, ordersSub.OrderId);
			mailCreate.MailContent = contentMail;
			mailCreate.RsvStatus = ordersSub.Status;
			mailCreate.SendTo = mailTemplate.SendTo;
			mailCreate.MailType = mailTemplate.Type;
			mailCreate.UpdateUserCd = updateUserCd;

			return mailCreate;
		}
		#endregion

		#region メール本文を作成する
		/// <summary>
		/// メール本文を作成する
		/// </summary>
		/// <param name="ordersSub"></param>
		/// <param name="mCustSub"></param>
		/// <param name="mCustrecieveSub"></param>
		/// <param name="orderDetails"></param>
		/// <param name="contentMailTemplate"></param>
		/// <param name="mBank"></param>
		/// <returns></returns>
		public string CreateContentMail(OrdersSub ordersSub,
										MCustSub mCustSub,
										MCustrecieveSub mCustrecieveSub,
										List<OrderDetail> orderDetails,
										string contentMailTemplate,
										MBank mBank = null)
		{
			string contentMail = string.Empty;
			string orderDetail = string.Empty;

			#region 予約詳細
			if (orderDetails.Count > 0)
			{
				for (int i = 0; i < orderDetails.Count; i++)
				{
					orderDetail += orderDetails[i].ProductCd + " / " +
											orderDetails[i].ProductName + " " +
											orderDetails[i].SizeName + " " +
											orderDetails[i].Quantity + " " +
											FormatPrice(orderDetails[i].Price) + " " +
											FormatPrice(orderDetails[i].SubTotal) + "<br/>";
				}
			}
			#endregion

			#region ご入金・お受取店舗
			string shopInfo = string.Empty;
			if (string.IsNullOrEmpty(ordersSub.ReceiveWay) ||
				(
					ordersSub.ReceiveWay == ParamConst.ReceiveWay.IN_SHOP_CD

				) ||
				(
					ordersSub.ReceiveWay == ParamConst.ReceiveWay.SHIPPING_CD &&
					ordersSub.PaymentWay == ParamConst.PaymentWay.PAY_IN_SHOP_CD
				)
			)
			{
				shopInfo = "<p> ■ご入金・お受取店舗<br/>" +
							"--------------------------------------------------------------------<br/>" +
							ordersSub.ShopName + "</p>";
			}
			#endregion

			#region 商品受取方法の詳細情報
			string receiveInfo = string.Empty;
			if (!string.IsNullOrEmpty(ordersSub.ReceiveWay) &&
				ordersSub.ReceiveWay == ParamConst.ReceiveWay.SHIPPING_CD)
			{
				receiveInfo = "<p>■配送<br/>" +
								"--------------------------------------------------------------------<br/>" +
								"お名前（漢字）： " + mCustrecieveSub.KanjiFamilyName + mCustrecieveSub.KanjiFirstName + "<br/>" +
								"お名前（かな）： " + mCustrecieveSub.KanaFamilyName + mCustrecieveSub.KanaFirstName + "<br/>" +
								"ご住所： 〒" + mCustrecieveSub.ZipCodeDsp + "&nbsp;" + mCustrecieveSub.ProvinceName + mCustrecieveSub.AdrCd1
											 + mCustrecieveSub.AdrCd2 + "&nbsp;" + mCustrecieveSub.AdrCd3 + "<br/>" +
								"お電話番号： " + mCustrecieveSub.PhoneNumber + "</p>";
			}
			#endregion

			#region 商品受け取り方法
			string receiveWay = string.Empty;
			string receiveWayName = string.Empty;
			if (!string.IsNullOrEmpty(ordersSub.ReceiveWay))
			{
				receiveWayName = ordersSub.ReceiveWay == ParamConst.ReceiveWay.SHIPPING_CD ?
										ParamConst.ReceiveWay.SHIPPING : ParamConst.ReceiveWay.IN_SHOP;
				receiveWay = "<p>■商品受け取り方法 　 " + receiveWayName + "</p>";
			}
			#endregion

			#region 決済方法
			string paymentWay = string.Empty;
			string paymentWayName = string.Empty;
			if (!string.IsNullOrEmpty(ordersSub.PaymentWay))
			{
				paymentWayName = ordersSub.PaymentWay == ParamConst.PaymentWay.TRANSFER_CD ?
										ParamConst.PaymentWay.TRANSFER : ParamConst.PaymentWay.PAY_IN_SHOP;
				paymentWay = "<p>■決済方法 　 " + paymentWayName + "</p>";
			}
			#endregion

			#region 早期割引特典
			string discount = string.Empty;
			string discountName = string.Empty;
			if (ordersSub.IsDiscount != null)
			{
				discountName = (bool)ordersSub.IsDiscount ? ParamConst.Discount.HAVE_DISCOUNT : ParamConst.Discount.NOT_DISCOUNT;
				discount = "<p>■早期割引特典 　 " + discountName + "</p>";
			}
			#endregion

			#region 振込先口座情報
			string bankInfo = string.Empty;
			if (mBank != null)
			{
				bankInfo = "<p>■振込先口座情報<br/>" +
							"--------------------------------------------------------------------<br/>" +
							"金融機関名： " + mBank.BankName + "<br/>" +
							"口座番号： " + mBank.AccountNumber + "<br/>" +
							"口座名義： " + mBank.AccountName + "<br/>" +
							"支店名： " + mBank.Branch + "</p>";
			}
			#endregion

			// メール本文

			contentMail = contentMailTemplate.Replace(ParamConst.ItemMailTemplate.MAIL_SPACE_HTML, string.Empty);

			// 名前（漢字）追加
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_KANJI_NAME, mCustSub.KanjiFamilyName + mCustSub.KanjiFirstName);

			// 予約番号追加
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_RSV_ID, ordersSub.OrderId);

			// 予約送信日時追加
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_RESERVE_DT_TM, ordersSub.CreateDtTm.ToString("yyyy/MM/dd HH:mm"));

			// ご予約合計金額
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_TOTAL, FormatPrice(ordersSub.Total));

			// 予約詳細追加
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_RESERVE_DETAIL, orderDetail);

			// 商品受け取り方法
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_RECEIVE_WAY, receiveWay);

			// 決済方法
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_PAYMENT_WAY, paymentWay);

			// ご入金・お受取店舗
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_SHOP_INFO, shopInfo);

			// 振込先口座情報
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_BANK_INFO, bankInfo);

			// 商品受取方法の詳細情報
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_RECEIVE_INFO, receiveInfo);

			// 名前（かな）追加
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_KANA_NAME, mCustSub.KanaFamilyName + mCustSub.KanaFirstName);

			// お客様住所追加
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_ZIP_CODE, mCustSub.ZipCodeDsp);
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_PROVINCE, mCustSub.ProvinceName);
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_ADR_CD1, mCustSub.AdrCd1);
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_ADR_CD2, mCustSub.AdrCd2);
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_ADR_CD3, mCustSub.AdrCd3);

			// 電話番号追加
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_PHONE_NUMBER, mCustSub.PhoneNumber);

			// メールアドレス追加
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_MAIL_ADDRESS, mCustSub.MailAddress);

			// 早期割引特典を追加する
			contentMail = contentMail.Replace(ParamConst.ItemMailTemplate.MAIL_DISCOUNT, discount);

			return contentMail;
		}
		#endregion

		#region パス画像
		/// <summary>
		/// パス画像
		/// </summary>
		/// <param name="bussinessName"></param>
		/// <param name="pathFolder"></param>
		/// <returns></returns>
		public string GetPathImageBarCd(string bussinessName, string pathFolder)
		{
			return Environment.CurrentDirectory + pathFolder + bussinessName.ToLower();
		}
		#endregion

		#region フォーマット価格
		/// <summary>
		/// フォーマット価格
		/// </summary>
		/// <returns>フォーマット価格</returns>
		public string FormatPrice(decimal price)
		{
			string outputPrice = price.ToString("#,##0.#####");

			return outputPrice;
		}
		#endregion
	}
}
