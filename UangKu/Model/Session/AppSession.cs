﻿namespace UangKu.Model.Session
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
    }
}
