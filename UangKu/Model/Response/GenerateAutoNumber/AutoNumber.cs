using UangKu.Model.Base;

namespace UangKu.Model.Response.GenerateAutoNumber
{
    public class AutoNumber
    {
        public class AutoNumberRoot : IResponse
        {
            public string AutoNumber { get; set; }
            public MetaData metaData { get; set; }
        }
    }
}
