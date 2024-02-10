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
        private static string ageminimum = string.Empty;
        public static string AgeMinimum { get => ageminimum; set => ageminimum = value; }
        private static string maxfilesize = string.Empty;
        public static string MaxFileSize { get => maxfilesize; set => maxfilesize = value; }
        private static string maxpicture = string.Empty;
        public static string MaxPicture { get => maxpicture; set => maxpicture = value; }
        private static string maxResult;
        public static string MaxResult { get => maxResult; set => maxResult = value; }
        private static string homemaxresult = string.Empty;
        public static string HomeMaxResult { get => homemaxresult; set => homemaxresult = value; }
        private static string timeout = string.Empty;
        public static string Timeout { get => timeout; set => timeout = value; }
        private static string showlastbuild = string.Empty;
        public static string ShowLastBuild { get => showlastbuild; set => showlastbuild = value; }
    }
}
