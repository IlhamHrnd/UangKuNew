namespace UangKu.Model.Base
{
    public class ParameterModel
    {
        public class Login
        {
            private static string status = "Aktif";
            public static string Status { get => status; set => status = value; }
        }

        public class DateFormat
        {
            private static DateTime datetime = DateTime.Now;
            public static DateTime DateTime { get => datetime; set => datetime = value; }
        }

        public class Report
        {
            private static string reportno = string.Empty;
            public static string ReportNo { get => reportno; set => reportno = value; }
        }

        public static class DateTimeFormat
        {
            private static string date = "dd/MM/yyyy";
            private static string datetime = "dd/MM/yyyy HH:mm";
            private static string datetimesecond = "dd/MM/yyyy HH:mm:ss";
            private static string datelong = "dd MMM yyyy";
            private static string dateshortmonth = "dd-MMM-yyyy";
            private static string longdatepattern = "dd-MMM-yyyy HH:mm:ss";
            private static string datehourminute = "dd/MM/yyyy HH:mm";
            private static string dateshortmonthhourminute = "dd-MMM-yyyy HH:mm";
            private static string hourmin = "HH:mm";
            private static string month = "MMMM";
            private static string yearmonthdate = "yyyy-MM-dd";
            private static string daydatemonthyear = "dddd, dd MMMM yyyy";

            public static string Date { get => date; set => date = value; }
            public static string Datetime { get => datetime; set => datetime = value; }
            public static string Datetimesecond { get => datetimesecond; set => datetimesecond = value; }
            public static string Datelong { get => datelong; set => datelong = value; }
            public static string Dateshortmonth { get => dateshortmonth; set => dateshortmonth = value; }
            public static string Longdatepattern { get => longdatepattern; set => longdatepattern = value; }
            public static string Datehourminute { get => datehourminute; set => datehourminute = value; }
            public static string Dateshortmonthhourminute { get => dateshortmonthhourminute; set => dateshortmonthhourminute = value; }
            public static string Hourmin { get => hourmin; set => hourmin = value; }
            public static string Month { get => month; set => month = value; }
            public static string Yearmonthdate { get => yearmonthdate; set => yearmonthdate = value; }
            public static string Daydatemonthyear { get => daydatemonthyear; set => daydatemonthyear = value; }
        }

        public class Transaction
        {
            private static decimal income = 0;
            private static decimal expenditure = 0;

            public static decimal Income { get => income; set => income = value; }
            public static decimal Expenditure { get => expenditure; set => expenditure = value; }
        }

        //Data Default Saat Gagal Ambil Data Dari AppParameter
        public class AppParameterDefault
        {
            private static string url = "https://uangkuapi.azurewebsites.net/";
            public static string URL { get => url; set => url = value; }
            private static int timeout = 10000; //Waktu Timeout Proses Ambil Data(Dalam Mili Detik)
            public static int TimeOut { get => timeout; set => timeout = value; }
        }

        public class ItemDefaultValue
        {
            private static string offline = "You're Offline";
            public static string Offline { get => offline; set => offline = value; }
            private static string online = "Back Online";
            public static string Online { get => online; set => online = value; }
            private static int firstpage = 1;
            public static int FirstPage { get => firstpage; set => firstpage = value; }
            private static bool isactive = true;
            public static bool IsActive { get => isactive; set => isactive = value; }
            private static bool isused = true;
            public static bool IsUsed {  get => isused; set => isused = value; }
            private static bool isdeleted = false;
            public static bool IsDeleted { get => isdeleted; set => isdeleted = value; }
            private static string newfile = "New";
            public static string NewFile { get => newfile; set => newfile = value; }
            private static string editfile = "Edit";
            private static string viewfile = "View";
            public static string ViewFile { get => viewfile; set => viewfile = value; }
            public static string EditFile { get =>  editfile; set => editfile = value; }
            private static string incometrans = "IN";
            public static string IncomeTrans { get => incometrans; set => incometrans = value; }
            private static string outcometrans = "OU";
            public static string OutcomeTrans { get => outcometrans; set => outcometrans = value; }
            private static string upload = "UPL";
            public static string Upload { get => upload; set => upload = value; }
            private static string userreport = string.Empty; //Report No Diambil Dari User > PersonID Dan User > Access
            public static string UserReport { get => userreport; set => userreport = value; }
            private static string wishlist = "WSL";
            public static string WishList { get => wishlist; set => wishlist = value; }
        }

        public class PermissionManager
        {
            public enum PermissionType
            {
                StorageRead,
                StorageWrite
            }
        }

        //Untuk Gambar Yang Ter Pick Satu Item
        public class ImageManager
        {
            private static string imagestring = string.Empty;
            public static string ImageString { get => imagestring; set => imagestring = value; }
            private static byte[] imagebyte = null;
            public static byte[] ImageByte { get => imagebyte; set => imagebyte = value; }
            private static string imagename = string.Empty;
            public static string ImageName { get => imagename; set => imagename = value; }
            private static string imageformat = string.Empty;
            public static string ImageFormat { get => imageformat; set => imageformat = value; }
            private static long imagesize = 0;
            public static long ImageSize { get => imagesize; set => imagesize = value; }
        }

        //Untuk Gambar Yang Ter Pick Beberapa Item
        public class ImageManagerList
        {
            public string ImageString { get; set; }
            public byte[] ImageByte { get; set; }
            public string ImageName { get; set; }
            public string ImageFormat { get; set; }
            public int ImageSize { get; set; }
        }
    }
}
