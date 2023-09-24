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
}
