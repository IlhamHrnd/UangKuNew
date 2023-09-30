using Newtonsoft.Json;

namespace UangKu.Model.Response.AppStandardReference
{
    public class AppStandardReferenceID
    {
        public class AppStandardReferenceIDRoot
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
