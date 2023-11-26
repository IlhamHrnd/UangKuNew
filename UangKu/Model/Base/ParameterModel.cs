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
            private static string format = "dd/MM/yyyy";
            public static string Format { get => format; set => format = value; }
            private static string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            public static string Date { get => date; set => date = value; }
            private static DateTime datetime = DateTime.Now;
            public static DateTime DateTime { get => datetime; set => datetime = value; }
            public static string MonthName { get => datetime.ToString("MMMM"); set => datetime.ToString("MMMM"); }
        }

        public class AppStandardReference
        {
            private static string itemid = string.Empty;
            public static string ItemID { get => itemid; set => itemid = value; }
        }

        public class AppStandardReferenceItem
        {
            private static string itemid = string.Empty;
            public static string ItemID { get => itemid; set => itemid = value; }
        }

        public class User
        {
            private static string userid = string.Empty;
            public static string UserID { get => userid; set => userid = value; }
        }

        public class ItemDefaultValue
        {
            private static string offline = "You're Offline";
            public static string Offline { get => offline; set => offline = value; }
            private static string online = "Back Online";
            public static string Online { get => online; set => online = value; }
            private static int firstpage = 1;
            public static int FirstPage { get => firstpage; set => firstpage = value; }
            private static int maxresult = 25;
            public static int Maxresult { get => maxresult; set => maxresult = value; }
            private static int homemaxresult = 5;
            public static int HomeMaxResult { get => homemaxresult; set => homemaxresult = value; }
            private static int timeout = 10000;
            public static int Timeout { get => timeout; set => timeout = value; }
            private static bool isactive = true;
            public static bool IsActive { get => isactive; set => isactive = value; }
            private static bool isused = true;
            public static bool IsUsed {  get => isused; set => isused = value; }
            private static bool isdeleted = false;
            public static bool IsDeleted { get => isdeleted; set => isdeleted = value; }
            private static long maxfilesize = 2 * 1024 * 1024; //Maximum File Size Is 2MB
            public static long MaxFileSize {  get => maxfilesize; set => maxfilesize = value; }
            private static string newfile = "New";
            public static string NewFile { get => newfile; set => newfile = value; }
            private static string editfile = "Edit";
            public static string EditFile { get =>  editfile; set => editfile = value; }
            private static string incometrans = "IN";
            public static string IncomeTrans { get => incometrans; set => incometrans = value; }
            private static string outcometrans = "OU";
            public static string OutcomeTrans { get => outcometrans; set => outcometrans = value; }
            private static string upload = "UPL";
            public static string Upload { get => upload; set => upload = value; }
            private static string currency = "id-ID";
            public static string Currency { get => currency; set => currency = value; }
        }

        public class PermissionManager
        {
            public enum PermissionType
            {
                StorageRead
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
