namespace UangKu.Model.Base
{
    internal interface IResponse
    {
        MetaData metaData { get; set; }
    }

    public class MetaData
    {
        public int code { get; set; }
        public bool isSucces { get; set; }
        public string message { get; set; }
    }
}
