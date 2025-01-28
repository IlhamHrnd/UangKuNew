namespace UangKu.WebService.Filter
{
    public class Root<T>
    {
        public T Data { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
