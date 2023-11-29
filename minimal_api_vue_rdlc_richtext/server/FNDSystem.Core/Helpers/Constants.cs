namespace FNDSystem.Core
{
    public static class Constants
    {
        public const string DELIMITER_CHARACTER = "@@@";
        public const string DELIMITER_NUMBER = ",";
        public const string NO_OUTPUT = "未出力";
        public const string MSG_REPRINT_JP = " は出力済です。再出力しますか？";
        public const string MSG_REPRINT_EN = " is outputed. Do you the it output?";
        public const string TITLE = "タイ";
        public const string RANGE = "範囲";
        public const string FACT = "事実";
        public const string CONCLUSION = "結論";
        public const string JAP_LANG = "J";
        public const string ENG_LANG = "E";
        public const string KIND_WC = "WC";
        public const string KIND_OBS = "OBS";
        public const string KIND_GFA = "GFA";
        public const string KIND_PFA = "PFA";
        public const string KIND_AFI = "AFI";
        public const string KIND_PD = "PD";
        public const string KIND_STR = "STR";
        public const string KIND_BP = "BP";
        public const string KIND_SOI = "SOI";
        public const string LETTER = "文字・";
        public const string PROCESS_CONTENT_OUTPUT = "出力";
        public const string PROCESS_DETAIL_CAPTURE = "取込";
        public const string PROCESS_DETAIL_UPDATE = "更新";
        public const string TRANS_SOURCE_PAST = "Past";
        public const string TRANS_SITUATION_CAPTURED = "取込済";
        public const string TRANS_SITUATION_PRINTED = "出力済";
        public const string CANCEL_STATUS_CONTINUE_TRANS = "翻訳継続";
        public const string CANCEL_STATUS_REQUESTING_CANCEL = "取消依頼中";
        public const string CANCEL_STATUS_CANCELED = "取消済";
        public const string TRANS_FULL_TEXT = "全文／Whole";
        public const string TRANS_PARTIAL_TEXT = "部分／Partial";
        public const string OBS_INPUT_LABEL_TITLE = "タイトル／Title";
        public const string OBS_INPUT_LABEL_SCOPE = "範囲／Scope";
        public const string OBS_INPUT_LABEL_FACT = "観察事実／Facts";
        public const string OBS_INPUT_LABEL_CONCLUSION = "結論／Conclusions";
        public const string STATIC_FILE_PATH = "FNDSystem.API\\wwwroot\\";
    }


    public static class RoleKbn
    {
        public static readonly string Administrator = "99";
        public static readonly string TenantOwner = "01";
        public static readonly string NormalUser = "02";
        public static readonly string[] Values = new string[] { "99", "01", "02" };
    }

    public static class LanguageCode
    {
        public static readonly string English = "E";
        public static readonly string Japanese = "J";
    }

    public static class FndReport
    {
        public static class Type
        {
            public const string PDF = "PDF";
            public const string WORD = "WORD";
            public const string EXCEL = "EXCEL";
        }

        public static class RenderType
        {
            public static readonly string PDF = "PDF";
            public static readonly string WORD = "WORDOPENXML";
            public static readonly string EXCEL = "EXCELOPENXML";
        }

        public static class Extension
        {

            public static readonly string PDF = ".pdf";
            public static readonly string WORD = ".docx";
            public static readonly string EXCEL = ".xlsx";
        }

        public static class ExportContentType
        {

            public static readonly string PDF = "application/pdf";
            public static readonly string WORD = "application/x-msexcel";
            public static readonly string EXCEL = "application/WORDOPENXML";
        }
    }

    public static class FormName
    {
        public static readonly string OBS = "OBS";
    }
}
