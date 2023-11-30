namespace Utility
{
	public static class ParamConst
	{
		/// <summary>
		/// 結果
		/// </summary>
		public static class ResultType
		{
			/// <summary>
			/// 失敗
			/// </summary>
			public const int ERROR = 0;
			/// <summary>
			/// 成功
			/// </summary>
			public const int SUCCESS = 1;
			/// <summary>
			/// 既に更新された
			/// </summary>
			public const int UPDATED = 2;
		}
		/// <summary>
		/// 削除フラグ
		/// </summary>
		public static class DelFlg
		{
			/// <summary>
			/// 削除した
			/// </summary>
			public const int ON = 1;
			/// <summary>
			/// 削除しない
			/// </summary>
			public const int OFF = 0;
		}

		/// <summary>
		/// 税
		/// </summary>
		public static double PRICE_TAX = 1.1;

		/// <summary>
		/// 住所タイプ
		/// </summary>
		public static class AdrType
		{
			/// <summary>
			/// お客様
			/// </summary>
			public const string ADR_CUST = "1";
			/// <summary>
			/// 店舗
			/// </summary>
			public const string ADR_SHOP = "2";
			/// <summary>
			/// お届け先
			/// </summary>
			public const string ADR_RECIEVE = "3";
		}

		/// <summary>
		/// メールタイプ
		/// </summary>
		public static class MailType
		{
			/// <summary>
			/// メールタイプ区分
			/// </summary>
			public static string KBN_CD = "0005";
			/// <summary>
			/// 予約控えメール（お客様／本部）見本
			/// </summary>
			public static string MAIL_ORDER = "10";
			/// <summary>
			/// 変更メール（お客様／本部）
			/// </summary>
			public static string MAIL_CHANGE = "11";
			/// <summary>
			/// キャンセルメール（お客様／本部）
			/// </summary>
			public static string MAIL_CANCEL = "12";
			/// <summary>
			/// 入金リマインドメール（お客様）
			/// </summary>
			public static string MAIL_REMIND = "20";
			/// <summary>
			/// 入金完了メール（お客様／本部）
			/// </summary>
			public static string MAIL_FINISH = "30";
		}

		/// <summary>
		/// 部署
		/// </summary>
		public static class Department
		{
			/// <summary>
			/// 本部
			/// </summary>
			public static int ADMIN = 1;
			/// <summary>
			/// 店舗
			/// </summary>
			public static int SHOP = 2;
		}

		/// <summary>
		/// 送信先
		/// </summary>
		public static class SendTo
		{
			/// <summary>
			/// お客様
			/// </summary>
			public static string SENDTO_CUSTOMER = "01";
			/// <summary>
			/// 管理者
			/// </summary>
			public static string SENDTO_ADMIN = "02";
		}

		/// <summary>
		/// 予約ステータス
		/// </summary>
		public static class ReserveStatus
		{
			/// <summary>
			/// 予約ステータス区分
			/// </summary>
			public static string KBN_CD = "0003";
			/// <summary>
			/// 予約
			/// </summary>
			public static string ORDER = "01";
			/// <summary>
			/// 入金完了
			/// </summary>
			public static string PAID = "02";
			/// <summary>
			/// 引渡し待ち
			/// </summary>
			public static string WAITING_DELIVERY = "03";
			/// <summary>
			/// 引渡し完了
			/// </summary>
			public static string COMPLETED_DELIVERY = "04";
			/// <summary>
			/// キャンセル
			/// </summary>
			public static string CANCEL = "05";
			/// <summary>
			/// 予約名
			/// </summary>
			public static string ORDER_TEXT = "予約(入金待ち)";
			/// <summary>
			/// 入金完了名
			/// </summary>
			public static string PAID_TEXT = "入金完了・引渡し完了";
			/// <summary>
			/// 引渡し完了名
			/// </summary>
			public static string COMPLETED_DELIVERY_TEXT = "引渡し完了";
			/// <summary>
			/// キャンセル名
			/// </summary>
			public static string CANCEL_TEXT = "キャンセル";
		}

		/// <summary>
		/// 履歴タイプ
		/// </summary>
		public static class Histype
		{
			/// <summary>
			/// ステータス更新
			/// </summary>
			public static string UPDATE_STATUS = "01";
			/// <summary>
			///  メール送信
			/// </summary>
			public static string SEND_MAIL = "02";
			/// <summary>
			/// 数量変更
			/// </summary>
			public static string CHANGE_QUANTITY = "03";
			/// <summary>
			/// サイズ変更
			/// </summary>
			public static string CHANGE_SIZE = "04";
			/// <summary>
			/// 色変更
			/// </summary>
			public static string CHANGE_COLOR = "05";
			/// <summary>
			/// サイズと数量を変更する
			/// </summary>
			public static string CHANGE_SIZE_AND_QUANTITY = "06";
			/// <summary>
			/// カラーと数量を変更する
			/// </summary>
			public static string CHANGE_COLOR_AND_QUANTITY = "07";
			/// <summary>
			/// サイズとカラーを変更する
			/// </summary>
			public static string CHANGE_SIZE_AND_COLOR = "08";
			/// <summary>
			/// カラー、サイズと数量を変更する
			/// </summary>
			public static string CHANGE_COLOR_AND_SIZE_AND_QUANTITY = "09";
			/// <summary>
			/// サイズ削除
			/// </summary>
			public static string DELETE_ORDER_DETAIL = "10";
			/// <summary>
			/// 店舗変更
			/// </summary>
			public static string CHANGE_SHOP = "11";
			/// <summary>
			/// 受取人情報を変更する
			/// </summary>
			public static string CHANGE_CUST_RECEIVE = "12";
		}

		/// <summary>
		/// メール送信状態
		/// </summary>
		public static class MailStatus
		{
			/// <summary>
			/// 送信成功
			/// </summary>
			public static string STATUS_SUCCESS = "01";
			/// <summary>
			/// 送信エラー
			/// </summary>
			public static string STATUS_ERROR = "02";
			/// <summary>
			/// 再送
			/// </summary>
			public static string STATUS_RESEND = "03";
			/// <summary>
			/// 未送信
			/// </summary>
			public static string STATUS_NOT_SEND = "04";
			/// <summary>
			/// メール送信成功ステータスのテキスト
			/// </summary>
			public static string STATUS_SUCCESS_TEXT = "送信済";
			/// <summary>
			/// メール送信失敗ステータスのテキスト
			/// </summary>
			public static string STATUS_ERROR_TEXT = "送信エラー";
			/// <summary>
			/// メール再送ステータスのテキスト
			/// </summary>
			public static string STATUS_RESEND_TEXT = "再送";
			/// <summary>
			/// 未送信
			/// </summary>
			public static string STATUS_NOT_SEND_TEXT = "未送信";
		}

		/// <summary>
		/// 注文
		/// </summary>
		public static class ResultOrders
		{
			/// <summary>
			/// 商品追加に失敗しました
			/// </summary>
			public static int ADD_PRODUCT_FAIL = 0;
			/// <summary>
			/// 在庫数を超える
			/// </summary>
			public static int PRODUCT_OUT_QUANTITY = -1;
		}

		/// <summary>
		/// インサートを結果
		/// </summary>
		public static class ResultInsert
		{
			/// <summary>
			/// 失敗する
			/// </summary>
			public static int INSERT_FAIL = 0;
		}

		public static class ResultInsertBrand
		{
			public static int INSERT_FAIL = -1;
		}
		public static class ResultInsertProduct
		{
			public static int INSERT_FAIL = -1;
		}

		public static class ImagePosition
        {
			public static string TOP = "01";
		}

		/// <summary>
		/// 早期割引特典
		/// </summary>
		public static class Discount
		{
			/// <summary>
			/// コード値引きあり
			/// </summary>
			public static int HAVE_DISCOUNT_CD = 1;
			/// <summary>
			/// コード値引きなし
			/// </summary>
			public static int NOT_DISCOUNT_CD = 0;
			/// <summary>
			/// 
			/// </summary>
			public static int NOT_CHOOSE_DISCOUNT_CD = 2;
			/// <summary>
			/// 値引きあり
			/// </summary>
			public static string HAVE_DISCOUNT = "あり";
			/// <summary>
			/// 値引きなし
			/// </summary>
			public static string NOT_DISCOUNT = "なし";
		}

		/// <summary>
		/// 商品受け取り方法
		/// </summary>
		public static class ReceiveWay
		{
			/// <summary>
			/// コード店舗
			/// </summary>
			public static string IN_SHOP_CD = "01";
			/// <summary>
			/// コード配送
			/// </summary>
			public static string SHIPPING_CD = "02";
			/// <summary>
			/// 店舗
			/// </summary>
			public static string IN_SHOP = "店頭受取";
			/// <summary>
			/// 配送
			/// </summary>
			public static string SHIPPING = "配送";
		}

		/// <summary>
		/// 決済方法
		/// </summary>
		public static class PaymentWay
		{
			/// <summary>
			/// コード店舗
			/// </summary>
			public static string PAY_IN_SHOP_CD = "01";
			/// <summary>
			/// コード配送
			/// </summary>
			public static string TRANSFER_CD = "02";
			/// <summary>
			/// 店舗
			/// </summary>
			public static string PAY_IN_SHOP = "店舗で支払い";
			/// <summary>
			/// 配送
			/// </summary>
			public static string TRANSFER = "振込";
		}

		/// <summary>
		/// 項目の表示・非表示
		/// </summary>
		public static class ShowHiddenItem
		{
			/// <summary>
			/// 表示
			/// </summary>
			public static int SHOW = 0;
			/// <summary>
			/// 表示
			/// </summary>
			public static string SHOW_TEXT = "表示";
			/// <summary>
			/// 非表示
			/// </summary>
			public static int HIDDEN = 1;
			/// <summary>
			/// 非表示
			/// </summary>
			public static string HIDDEN_TEXT = "非表示";
		}

		/// <summary>
		/// 免税使用
		/// </summary>
		public static class UsedDutyFree
		{
			/// <summary>
			/// あり
			/// </summary>
			public static int FREE = 0;
			/// <summary>
			/// あり
			/// </summary>
			public static string FREE_TEXT = "あり";
			/// <summary>
			/// なし
			/// </summary>
			public static int NOT_FREE = 1;
			/// <summary>
			/// なし
			/// </summary>
			public static string NOT_FREE_TEXT = "なし";
		}

		/// <summary>
		/// 都道府県
		/// </summary>
		public static class Province
		{
			/// <summary>
			/// 都道府県区分
			/// </summary>
			public static string KBN_CD = "0001";
		}

		/// <summary>
		/// エリア2
		/// </summary>
		public static class Area2
		{
			/// <summary>
			/// 区分コード
			/// </summary>
			public static string KBN_CD = "0002";
		}

		/// <summary>
		/// エリア
		/// </summary>
		public static class Area
		{
			/// <summary>
			/// 区分コード
			/// </summary>
			public static string KBN_CD = "0010";
		}

		/// <summary>
		/// 契約形態
		/// </summary>
		public static class ContractType
		{
			/// <summary>
			/// 区分コード
			/// </summary>
			public static string KBN_CD = "0011";
		}

		/// <summary>
		/// BarCodeフォルダーのパス
		/// </summary>
		public static string PATH_BAR_CODE_IMAGE = "\\Images\\bar-code\\";

		/// <summary>
		/// 開店ステータス
		/// </summary>
		public static class ShopStatus
		{
			/// <summary>
			/// オープン
			/// </summary>
			public static int OPEN = 0;
			/// <summary>
			/// オープン名
			/// </summary>
			public static string OPEN_TEXT = "出店";
			/// <summary>
			/// クローズ
			/// </summary>
			public static int CLOSE = 1;
			/// <summary>
			/// クローズ名
			/// </summary>
			public static string CLOSE_TEXT = "退店";
		}

		/// <summary>
		/// 画面テキスト区分
		/// </summary>
		public static class ScreenType
		{
			/// <summary>
			/// 区分コード
			/// </summary>
			public static string KBN_CD = "0012";
			/// <summary>
			/// TOP画面
			/// </summary>
			public static string TOP = "01";
			/// <summary>
			/// 商品一覧画面
			/// </summary>
			public static string PRODUCT_LIST = "02";
			/// <summary>
			/// 商品詳細画面
			/// </summary>
			public static string PRODUCT_DETAIL = "03";
			/// <summary>
			/// 予約情報入力画面
			/// </summary>
			public static string ORDER_INPUT = "04";
			/// <summary>
			/// 予約情報確認画面
			/// </summary>
			public static string CONFIRM_ORDER_INPUT = "05";
			/// <summary>
			/// 予約登録完了画面
			/// </summary>
			public static string ORDER_FINISH = "06";
			/// <summary>
			/// 法表記
			/// </summary>
			public static string RULE = "07";
		}

		/// <summary>
		/// パスワード
		/// </summary>
		public static class PassWord
		{
			/// <summary>
			/// 区分コード
			/// </summary>
			public static string KBN_CD = "0013";
		}

		/// <summary>
		/// List ignore check path
		/// </summary>
		public static string[] LST_COMMON_PATH = {
			"/admin/api/MBussiness/GetAll",
			"/admin/api/Menu/GetMenusByUser",
			"/admin/api/Common/GetCbbByKbnCd"
		};

		/// <summary>
		/// バウンス情報
		/// </summary>
		public static class MailBounce
		{
			/// <summary>
			/// メールアドレスが存在しない・無効/不正
			/// </summary>
			public static string MAIL_EXIST = "5.1.1";
			/// <summary>
			/// メールアドレスが存在しない・無効/不正名
			/// </summary>
			public static string MAIL_EXIST_TEXT = "メールアドレスが存在しない・無効/不正";
			/// <summary>
			/// メールボックスがいっぱい
			/// </summary>
			public static string MAIL_FULL = "5.2.2";
			/// <summary>
			/// メールボックスがいっぱい名
			/// </summary>
			public static string MAIL_FULL_TEXT = "メールボックスがいっぱい";
			/// <summary>
			/// メールが拒否された
			/// </summary>
			public static string MAIL_REJECT = "5.6.1";
			/// <summary>
			/// メールが拒否された名
			/// </summary>
			public static string MAIL_REJECT_TEXT = "メールが拒否された";
			/// <summary>
			/// その他
			/// </summary>
			public static string OTHER = "その他";
		}

		public static class MenuId
		{
			public const int MANAGE_ORDER = 1;
			public const int MANAGE_STOCK = 2;
			public const int MANAGE_MAIL = 5;
			public const int MANAGE_SCREEN = 6;
		}

		public static string PATH_IMAGE_PRODUCT_DETAIL = "\\Images\\product-detail\\";
		public static string PATH_IMAGE_PRODUCT_LIST = "\\Images\\product-list\\";
		public static string PATH_IMAGE_SCREEN = "\\Images\\top\\";
		public static class IdxDataCsv
		{
			public const int PRODUCT_CD = 0;
			public const int SIZE_NAME = 1;
			public const int COLOR_NAME = 2;
			public const int YEAR = 3;
			public const int PRODUCT_NAME = 4;
			public const int PRODUCT_NAME1 = 5;
			public const int BRAND_NAME = 6;
			public const int BRAND_SUB = 7;
			public const int INVENTORY_NUMBER = 8;
			public const int PRICE = 9;
			public const int DISPLAY = 10;
		}

		public static class IdxDataCsvUser
		{
			public const int USER_CD = 0;
			public const int USER_NAME_KANJI = 1;
			public const int USER_NAME_KANA = 2;
			public const int EMAIL_ADDRESS = 3;
			public const int ROLE_NAME = 4;
			public const int PASSWORD = 5;
			public const int BUSSINESS_NAME = 6;
		}

		public static class IdxDataCsvShop
		{
			public const int SHOP_CD = 0;
			public const int SHOP_NAME = 1;
			public const int ZIP_CD = 2;
			public const int PROVINCE = 3;
			public const int ADR_CD_1 = 4;
			public const int ADR_CD_2 = 5;
			public const int AREA2_CD = 6;
			public const int AREA_CD = 7;
			public const int MAIL_ADDRESS = 8;
			public const int PHONE_NUMBER = 9;
			public const int FAX = 10;
			public const int STATUS = 11;
			public const int START_DATE = 12;
			public const int END_DATE = 13;
			public const int AFFILIATE_DEPARTMENT_CD = 14;
			public const int AFFILIATE_DEPARTMENT_NAME = 15;
			public const int OPEN_TIME = 16;
			public const int SQUARE = 17;
			public const int SALE_PERSON_CD = 18;
			public const int SALE_PERSON_NAME = 19;
			public const int USED_DUTY_FREE = 20;
			public const int CONTRACT_TYPE = 21;
			public const int DISPLAY_FLG = 22;
			public const int BUSSINESS_CD = 23;
			public const int TOTAL_COLUMN = 24;
		}

		public static class TextItem
		{
			public static string FILE_CSV = "CSV";
			public static string SHOP_CD = "店舗コード";
			public static string SHOP_NAME = "店舗名";
			public static string ZIP_CD = "郵便番号";
			public static string PROVINCE = "都道府県";
			public static string ADR_CD1_SHOP = "所在地１";
			public static string ADR_CD2_SHOP = "所在地２";
			public static string AREA2 = "エリア２";
			public static string AREA = "エリア名";
			public static string MAIL_ADDRESS = "メールアドレス";
			public static string TEL = "TEL";
			public static string FAX = "FAX";
			public static string STORE_STATUS = "出退店区分";
			public static string START_DATE = "開店日";
			public static string END_DATE = "閉店日";
			public static string AFFILIATE_DEPARTMENT_CD = "所属部門";
			public static string AFFILIATE_DEPARTMENT_NAME = "所属部門名";
			public static string OPEN_TIME = "営業時間";
			public static string SQUARE = "坪数";
			public static string SALE_PERSON_CD = "営業担当者";
			public static string SALE_PERSON_NAME = "営業担当者名";
			public static string USED_DUTY_FREE = "免税使用";
			public static string CONTRACT_TYPE = "契約形態";
			public static string DISPLAY_FLG = "表示ステータス";
			public static string BUSSINESS_CD = "業態コード";
			public static string SKU = "SKU";
			public static string PRODUCT_CD = "品番";
			public static string SIZE_NAME = "サイズ名";
			public static string COLOR_NAME = "色名";
			public static string YEAR = "年";
			public static string PRODUCT_NAME = "商品名";
			public static string PRODUCT_NAME1 = "商品名1";
			public static string BRAND_CD = "ブランドコード";
			public static string BRAND_NAME = "ブランド名";
			public static string BRAND_SUB = "サブブランド";
			public static string INVENTORY_NUMBER = "在庫";
			public static string PRICE = "価格";
			public static string DISPLAY = "表示";
			public static string USER_CD = "ユーザーコード";
			public static string USER_NAME_KANJI = "名前（漢字）";
			public static string USER_NAME_KANA = "名前（かな）";
			public static string ROLE_NAME = "権限名";
			public static string PASSWORD = "パスワード";
			public static string BUSSINESS_NAME = "業態名";
			public static string SUPER_ADMIN_ROLE_NAME = "特権管理者";
		}

		public static class MessageError
		{
			public const string MSG_ERROR_SKU = "SKUの形式が正しくありません。\n";
			public const string MSG_ERROR_PRODUCT_CD = "品番の形式が正しくありません。\n";
			public const string MSG_ERROR_SIZE = "サイズがマスタに存在していません。\n";
			public const string MSG_ERROR_COLOR = "カラーがマスタに存在していません。\n";
			public const string MSG_ERROR_YEAR = "年の形式が正しくありません。\n";
			public const string MSG_ERROR_PRODUCT_NAME = "商品名の形式が正しくありません。\n";
			public const string MSG_ERROR_PRODUCT_NAME1 = "商品名1の形式が正しくありません。\n";
			public const string MSG_ERROR_BRAND_CD = "ブランドコードの形式が正しくありません。\n";
			public const string MSG_ERROR_BRAND_NAME = "ブランド名の形式が正しくありません。。\n";
			public const string MSG_ERROR_BRAND_SUB = "サブブランド名の形式が正しくありません。\n";
			public const string MSG_ERROR_INVENTORY_NUMBER = "在庫の形式が正しくありません。\n";
			public const string MSG_ERROR_PRICE = "価格の形式が正しくありません。。\n";
			public const string MSG_ERROR_DISPLAY = "表示の形式が正しくありません。\n";
			public const string MSG_ERROR_USER_CD = "ユーザーコードのフォーマットが正しくありません。 \n";
			public const string MSG_ERROR_USER_NAME_KANJI = "名前（漢字）のフォーマットが正しくありません。 \n";
			public const string MSG_ERROR_USER_NAME_KANA = "名前（かな）のフォーマットが正しくありません。 \n";
			public const string MSG_ERROR_EMAIL = "メールのフォーマットが正しくありません。 \n";
			public const string MSG_ERROR_ROLE = "権限のフォーマットが正しくありません。 \n";
			public const string MSG_ERROR_PASSWORD = "パスワードのフォーマットが正しくありません。 \n";
			public const string MSG_ERROR_BUSSINESS_CD = "業態コードのフォーマットが正しくありません。 \n";
		}

		public static class Regex
		{
			public static string REGEX_NUMBER = "^[0-9]*$";
			public static string REGEX_SPECIAL_ADDRESS = "^[ｧ-ﾝﾞﾟ一-龠ぁ-ゔァ-ヴーa-zA-Z0-9ａ-ｚＡ-Ｚ０-９々〆〤ァ-ヶぁ-ゞｦ-ﾟ/\\-\\－（）()／\\ \\　]+$";
			public static string REGEX_MAIL = @"^[a-zA-Z0-9.!#$%&'*+\\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
			public static string REGEX_TEL_FAX = "^[0-9-]*$";
			public static string REGEX_FLOAT = @"^[0-9]*(?:\.[0-9]*)?$";
			public static string REGEX_TIME = "^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";
			public static string REGEX_KANA = "^[ぁ-んァ-ンｱ-ﾝ]+$";
			public static string REGEX_USER_PASSWORD = "^[a-zA-Z0-9]+$";
		}
		public static string SCREEN_PRODUCT_DETAIL = "03";
		public static class RoleDetail
		{
			public static string VIEW = "VIEW";
			public static string UPD = "UPD";
			public static string DOWN_CSV = "DOWN_CSV";
			public static string SHOP_REPRESENT = "SHOP_REPRESENT";
		}
		public static class Role
		{
			public static int SUPER_ADMIN = 1;
		}

		public static class ItemMailTemplate
		{
			public static string MAIL_RSV_ID = "{{RsvId}}";
			public static string MAIL_KANJI_NAME = "{{KanjiName}}";
			public static string MAIL_RESERVE_DT_TM = "{{ReserveDtTm}}";
			public static string MAIL_TOTAL = "{{Total}}";
			public static string MAIL_RESERVE_DETAIL = "{{ReserveDetail}}";
			public static string MAIL_RECEIVE_WAY = "{{ReceiveWayName}}";
			public static string MAIL_PAYMENT_WAY = "{{PaymentWayName}}";
			public static string MAIL_SHOP_INFO = "{{ShopInfo}}";
			public static string MAIL_BANK_INFO = "{{BankInfo}}";
			public static string MAIL_RECEIVE_INFO = "{{ReceiveInfo}}";
			public static string MAIL_KANA_NAME = "{{KanaName}}";
			public static string MAIL_ZIP_CODE = "{{ZipCode}}";
			public static string MAIL_PROVINCE = "{{Province}}";
			public static string MAIL_ADR_CD1 = "{{AdrCd1}}";
			public static string MAIL_ADR_CD2 = "{{AdrCd2}}";
			public static string MAIL_ADR_CD3 = "{{AdrCd3}}";
			public static string MAIL_PHONE_NUMBER = "{{PhoneNumber}}";
			public static string MAIL_MAIL_ADDRESS = "{{MailAddress}}";
			public static string MAIL_DISCOUNT = "{{Discount}}";
			public static string MAIL_IMAGE_BAR_CD = "{{ImageBarCd}}";
			public static string MAIL_SPACE_HTML = "&nbsp;";
		}

		public static string CHARACTER_ADD = "〒";
	}
}
