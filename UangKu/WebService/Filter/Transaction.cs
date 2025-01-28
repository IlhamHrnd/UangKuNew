namespace UangKu.WebService.Filter
{
    public class Transaction
    {
        public string TransType { get; set; }
        public string PersonID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string OrderBy { get; set; }
        public bool? IsAscending { get; set; }
        public string TransNo { get; set; }
    }
}
