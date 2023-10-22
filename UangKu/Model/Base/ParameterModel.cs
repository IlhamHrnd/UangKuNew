namespace UangKu.Model.Base
{
    public class ParameterModel
    {
        public class Login
        {
            private static string status = "Aktif";
            public static string Status { get => status; set => status = value; }
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

        public class ItemDefaultValue
        {
            private static string offline = "You're Offline";
            public static string Offline { get => offline; set => offline = value; }
            private static string online = "Back Online";
            public static string Online { get =>  online; set => online = value; }
            private static int maxresult = 25;
            public static int Maxresult { get => maxresult; set => maxresult = value; }
            private static int timeout = 10000;
            public static int Timeout { get => timeout; set => timeout = value; }
            private static string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            public static string Date { get => date; set => date = value; }
            private static DateTime datetime = DateTime.Now;
            public static DateTime DateTime { get => datetime; set => datetime = value; }
            private static bool isactive = true;
            public static bool IsActive { get => isactive; set => isactive = value; }
            private static bool isused = true;
            public static bool IsUsed {  get => isused; set => isused = value; }
        }

        public class PermissionManager
        {
            public enum PermissionType
            {
                StorageRead
            }
        }
    }
}
