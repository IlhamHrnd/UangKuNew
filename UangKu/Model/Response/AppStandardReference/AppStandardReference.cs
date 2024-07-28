using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.AppStandardReference
{
    public class AppStandardReference
    {
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

        public class AppStandardReferenceRoot : IResponse
        {
            [JsonProperty("pageNumber")]
            public int? pageNumber { get; set; }

            [JsonProperty("pageSize")]
            public int? pageSize { get; set; }

            [JsonProperty("totalPages")]
            public int? totalPages { get; set; }

            [JsonProperty("totalRecords")]
            public int? totalRecords { get; set; }

            [JsonProperty("prevPageLink")]
            public object prevPageLink { get; set; }

            [JsonProperty("nextPageLink")]
            public object nextPageLink { get; set; }

            [JsonProperty("data")]
            public List<Datum> data { get; set; }

            [JsonProperty("succeeded")]
            public bool? succeeded { get; set; }

            [JsonProperty("errors")]
            public object errors { get; set; }

            [JsonProperty("message")]
            public object message { get; set; }
            public MetaData metaData { get; set; }
        }
    }
}
