using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.AppStandardReference
{
    public class AppStandardReferenceID
    {
        public class AppStandardReferenceIDRoot : IResponse
        {
            public Datum data { get; set; }
            public MetaData metaData { get; set; }
        }
        public class Datum
        {
            [JsonProperty("standardReferenceID")]
            public string standardReferenceID { get; set; }

            [JsonProperty("standardReferenceName")]
            public string standardReferenceName { get; set; }

            [JsonProperty("itemLength")]
            public int? itemLength { get; set; }

            [JsonProperty("isUsedBySystem")]
            public bool? isUsedBySystem { get; set; }

            [JsonProperty("isActive")]
            public bool? isActive { get; set; }

            [JsonProperty("note")]
            public string note { get; set; }

            [JsonProperty("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonProperty("lastUpdateByUserID")]
            public string lastUpdateByUserID { get; set; }
        }
    }
}
