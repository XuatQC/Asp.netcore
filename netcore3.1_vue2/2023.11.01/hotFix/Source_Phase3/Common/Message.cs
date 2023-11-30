using System;

namespace Utility
{
	public static class Message
	{
		#region 通知の*の代用
		/// <summary>
		/// 通知の*の代用 
		/// </summary>
		/// <param name="msgContent">メッセージ内容（内容に*を含む）</param>
		/// <param name="replacement">*の内容</param>
		/// <returns>代用した後のメッセージ</returns>
		public static string GetMessageInfo(string msgContent, params String[] replacement)
		{
			string modMessage;
			int nowIndex = 0;
			int listCnt = 0;
			modMessage = msgContent.Replace("$", "\r\n");

			if (replacement.Length != 0)
			{
				do
				{
					nowIndex = modMessage.IndexOf("*", nowIndex);

					if (nowIndex == -1)
					{
						break;
					}

					if (replacement.Length - 1 < listCnt)
					{
						break;
					}

					modMessage = modMessage.Remove(nowIndex, 1);
					modMessage = modMessage.Insert(nowIndex, replacement[listCnt].ToString());

					listCnt++;
					nowIndex++;
				}
				while (true);
			}

			modMessage = modMessage.Replace("*", string.Empty);

			return modMessage;
		}
		#endregion

		#region 共通通知
		/// <summary>
		/// 共通通知
		/// </summary>
		public static class Common
		{
			// Please input *
			public static readonly string MSG_IMPUT = "*を入力してください。";

			// * wrong format
			public static readonly string MSG_WRONG_FMT = "*のフォーマットが正しくありません。";

			// * Successed (Login, save, update, delete, register...)
			public static readonly string MSG_SUCCESSED = "*に成功しました。";

			// * Failed (Login, save, update, delete, register...)
			public static readonly string MSG_FAILED = "*に失敗しました。";

			// * Can not contain special character
			public static readonly string MSG_SPECIAL = "*に特殊記号を許せません。";

			// * already exist
			public static readonly string MSG_ALREADY_EXIST = "*が既に存在しています。";

			// * not exist
			public static readonly string MSG_NOT_EXIST = "*が存在していません。";

			// Already updated by another person
			public static readonly string MSG_ALREADY_UPDATE = "既に更新されました";

			// * infor did not correct
			public static readonly string MSG_NOT_CORRECT_INFO = "*が正しくありません。";

			// * infor was wrong
			public static readonly string MSG_NOT_WRONG_INFO = "*が間違っています。";

			// When expired date
			public static readonly string MSG_OUT_OF_PERIOD = "対象期間外のため、*できません。";

			// do not have permission to access funtion 
			public static readonly string MSG_FUNC_ACCESS_DENIE = "この機能を使用権限がありません";

			// * length too long
			public static readonly string MSG_LENGTH_TOO_LONG = "*$length文字以内で入力してください。";

			// * input format
			public static readonly string MSG_INPUT_FORMAT = "*のフォーマットが正しくありません。";
			public static readonly string MSG_ERROR_SKU = "品番+サイズ名+色名が重複しています。";
			public static readonly string MSG_ERROR_SIZE = "サイズ名称がサイズコードに該当していません。";
			public static readonly string MSG_ERROR_COLOR = "カラー名称がカラーコードに該当していません。";
			public static readonly string MSG_ERROR_YEAR = "年はYYYY形式で入力してください。";
			public static readonly string MSG_ERROR_INVENTORY_NUMBER = "在庫数は0以外の半角数字を入力してください。";
			public static readonly string MSG_ERROR_PRICE = "価格（税抜き）は半角数字を入力してください。";
			public static readonly string MSG_ERROR_WRONG_PRICE = "価格（税抜き）は０以外の半角数字を入力してください。";
			public static readonly string MSG_ERROR_DISPLAY = "表示ステータスは「表示」又は「非表示」を入力してください。";
			public static readonly string MSG_ERROR_USER_CD = "ユーザーコードは英数字の8桁で入力してください。";
			public static readonly string MSG_ERROR_PASSWORD = "パスワードは英数字の8桁で入力してください。";
			public static readonly string MSG_ERROR_USER_NAME_KANA = "名前（かな）はひらがなを入力してください。";
			public static readonly string MSG_ERROR_BRAND_NAME = "ブランド名がブランドマスタに存在していません。";
			public static readonly string MSG_ERROR_SUB_BRAND = "サブブランドを入力してください。";
			public static readonly string MSG_ERROR_ROLE_NAME = "「特権管理者」は全て業態を管理できるので、業態名を入力しないでください。";
			public static readonly string MSG_ERROR_BUSSINESS = "業態名を入力してください。";
			public static readonly string MSG_ERROR_PRODUCT_CD = "品番が既に存在しています。";

			// error size was order
			public static readonly string MSG_ERROR_SIZE_WAS_ORDER = "予約があったため、更新又は削除できません。";

			// error no data
			public static readonly string MSG_ERROR_NO_DATA = "データがありません。";

			// user was deleted
			public static readonly string MSG_USER_WAS_DELETED = "システムで削除されたユーザーコードが存在します。";

			// Please enter one
			public static readonly string MSG_PLEASE_ENTER_ONE = "いずれかを入力してください。";

			// please enter one
			public static readonly string MSG_PLEASE_ENTER = "$itemは$valueの$text";

			// please input one
			public static readonly string MSG_PLEASE_IMPUT_ONE = "$item$valueを入力してください。";
		}
		#endregion

		#region ログイン画面の通知
		/// <summary>
		/// ログイン画面の通知
		/// </summary>
		public static class Login
		{
			// Access from outsite admin network
			public static readonly string MSG_IP_ACCESS_DENIE = "ログインする権限がありません";

			// Login infor did not correct
			public static readonly string MSG_WRONG_INFO = "ログイン情報が正しくありません。";

			// User hasbeen deleted
			public static readonly string MSG_USER_DELETED = "アカウントが削除されました。";
		}
		#endregion

		#region 予約詳細
		/// <summary>
		/// 予約詳細
		/// </summary>
		public static class OrderDetail
		{
			// Not enough stock
			public static readonly string MSG_WRONG_INFO = "*の在庫数が足りません。数量を再度入力してください。";

			// 商品が存在しない
			public static readonly string MSG_PRODUCT_NOT_EXIST = "選択されたサイズとカラーがある商品を見つけません。再度確認してください。";
		}
		#endregion

		public static class MShop
		{
			// Date open than date close
			public static readonly string MSG_DATE_OPEN_THAN_DATE_CLOSE = "閉店日は開店日以降の日付で入力してください。";

			// Time open than time close
			public static readonly string MSG_TIME_OPEN_THAN_TIME_CLOSE = "終了時間は開始時間以降の時間で入力してください。";

			// Open time format is incorrect
			public static readonly string MSG_OPEN_TIME_INCORRECT = "*はhh:mm-hh:mm形式で入力してください。";

			public static readonly string MSG_PAYMENT_WAY_NOT_VALID = "支払方法が有効ではありません。";

			// area2
			public static readonly string MSG_AREA2_VALUE = "「関東」「東海・北陸」「北海道」「東北」「近畿」「中国・四国」「九州・沖縄」";

			// area
			public static readonly string MSG_AREA_VALUE = "「NET(その他)」「その他」「西日本」「東日本」「九州」「未使用（元：EC店）」";

			// Status open/close
			public static readonly string MSG_STATUS_VALUE = "は「退店」又は「出店」";

			// Used duty free
			public static readonly string MSG_USED_DUTY_FREE_VALUE = "は「あり」又は「なし」";

			// Contract type
			public static readonly string MSG_CONTRACT_TYPE_VALUE = "「ＦＣ]「直営」「その他」";

			// Display flg
			public static readonly string MSG_DISPLAY_FLG_VALUE = "は「表示」又は「非表示」";

		}
	}
}
