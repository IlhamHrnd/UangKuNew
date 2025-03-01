namespace UangKu.Model.Base
{
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

    public class ItemManager
    {
        private static string url = "https://uangku.runasp.net/";
        public static string Url { get => url; set => url = value; }
        private static int timeout = 10;
        public static int Timeout { get => timeout; set => timeout = value; }
        private static string offline = "You're Offline";
        public static string Offline { get => offline; set => offline = value; }
        private static string online = "Back Online";
        public static string Online { get => online; set => online = value; }
        private static int firstpage = 1;
        public static int FirstPage { get => firstpage; set => firstpage = value; }
        private static bool isactive = true;
        public static bool IsActive { get => isactive; set => isactive = value; }
        private static bool isused = true;
        public static bool IsUsed { get => isused; set => isused = value; }
        private static bool isdeleted = false;
        public static bool IsDeleted { get => isdeleted; set => isdeleted = value; }
        private static string newfile = "New";
        public static string NewFile { get => newfile; set => newfile = value; }
        private static string editfile = "Edit";
        public static string EditFile { get => editfile; set => editfile = value; }
        private static string viewfile = "View";
        public static string ViewFile { get => viewfile; set => viewfile = value; }
        private static string deletefile = "Delete";
        public static string DeleteFile { get => deletefile; set => deletefile = value; }
        private static string status = "Status-001";
        public static string Status { get => status; set => status = value; }
        private static string orderby = "OrderByTransaction-003";
        public static string OrderBy { get => orderby; set => orderby = value; }
        private static string empty = "Select One Item";
        public static string Empty { get => empty; set => empty = value; }
        private static string cancel = "Cancel";
        public static string Cancel { get => cancel; set => cancel = value; }
    }

    public class AutoNumberType
    {
        private static string income = "IN";
        private static string expenditure = "OU";
        private static string doc = "DOC";
        private static string pdf = "PDF";
        private static string upl = "UPL";
        private static string rpt = "RPT";
        private static string wsl = "WSL";

        public static string Income { get => income; set => income = value; }
        public static string Expenditure { get => expenditure; set => expenditure = value; }
        public static string Doc { get => doc; set => doc = value; }
        public static string Pdf { get => pdf; set => pdf = value; }
        public static string Upl { get => upl; set => upl = value; }
        public static string Rpt { get => rpt; set => rpt = value; }
        public static string Wsl { get => wsl; set => wsl = value; }
    }

    public class PermissionManager
    {
        public enum PermissionType
        {
            StorageRead,
            StorageWrite
        }
    }

    public class AppProgram
    {
        public const string HomePage = "PRG/PRG/250201-001";
        public const string Profile = "PRG/PRG/250202-001";
        public const string Transaction = "PRG/PRG/250202-002";
        public const string EditProfile = "PRG/PRG/250209-001";
    }

    //Untuk Gambar Yang Ter Pick
    public class ImageManager
    {
        private string imagestring = string.Empty;
        public string ImageString { get => imagestring; set => imagestring = value; }
        private byte[] imagebyte = null;
        public byte[] ImageByte { get => imagebyte; set => imagebyte = value; }
        private string imagename = string.Empty;
        public string ImageName { get => imagename; set => imagename = value; }
        private static string imageformat = string.Empty;
        public string ImageFormat { get => imageformat; set => imageformat = value; }
        private static long imagesize = 0;
        public long ImageSize { get => imagesize; set => imagesize = value; }
    }
}
