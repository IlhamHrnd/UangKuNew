namespace UangKu.Model.Session
{
    public class AppSession
    {
        public string username { get; set; }
        public string sexName { get; set; }
        public string accessName { get; set; }
        public string statusName { get; set; }
        public DateTime? activeDate { get; set; }
        public DateTime? lastLogin { get; set; }
        public DateTime? lastUpdateDateTime { get; set; }
        public string lastUpdateByUser { get; set; }
        public string personID { get; set; }
    }

    public class AppAccess
    {
        public bool IsAdmin { get; set; }
        public bool IsAdult { get; set; }
    }

    public class AppParameter
    {
        private static int ageminimum = 0;
        public static int AgeMinimum { get => ageminimum; set => ageminimum = value; }
        private static int maxfilesize = 0;
        public static int MaxFileSize { get => maxfilesize; set => maxfilesize = value; }
        private static int maxpicture = 0;
        public static int MaxPicture { get => maxpicture; set => maxpicture = value; }
        private static int maxResult;
        public static int MaxResult { get => maxResult; set => maxResult = value; }
        private static int homemaxresult = 0;
        public static int HomeMaxResult { get => homemaxresult; set => homemaxresult = value; }
        private static int timeout = 0;
        public static int Timeout { get => timeout; set => timeout = value; }
        private static bool showlastbuild = false;
        public static bool ShowLastBuild { get => showlastbuild; set => showlastbuild = value; }
        private static bool isallowcustomdate = false;
        public static bool IsAllowCustomDate { get => isallowcustomdate; set => isallowcustomdate = value; }
        private static string url = string.Empty;
        public static string URL { get => url; set => url = value; }
        private static string numericformat = string.Empty;
        public static string NumericFormat { get => numericformat; set => numericformat = value; }
        private static string currencyformat = string.Empty;
        public static string CurrencyFormat { get => currencyformat; set => currencyformat = value; }
        private static string downloadFolder = string.Empty;
        public static string DownloadFolder { get => downloadFolder; set => downloadFolder = value; }
        private static string blankpdf = string.Empty;
        public static string BlankPDF { get => blankpdf; set => blankpdf = value; }
        private static bool isaddpagenumber = false;
        public static bool IsAddPageNumber { get => isaddpagenumber; set => isaddpagenumber = value; }
        private static bool isaddheader = false;
        public static bool IsAddHeader { get => isaddheader; set => isaddheader = value; }
        private static bool isaddfooter = false;
        public static bool IsAddFooter { get => isaddfooter; set => isaddfooter = value; }
        private static string pdfpagesize = string.Empty;
        public static string PDFPageSize { get => pdfpagesize; set => pdfpagesize = value; }
    }
}
