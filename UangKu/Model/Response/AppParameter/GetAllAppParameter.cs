using Newtonsoft.Json;

namespace UangKu.Model.Response.AppParameter
{
    public class GetAllAppParameter
    {
        public class Datum
        {
            [JsonProperty("parameterID")]
            public string parameterID { get; set; }

            [JsonProperty("parameterName")]
            public string parameterName { get; set; }

            [JsonProperty("parameterValue")]
            public string parameterValue { get; set; }

            [JsonProperty("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonProperty("lastUpdateByUserID")]
            public string lastUpdateByUserID { get; set; }

            [JsonProperty("isUsedBySystem")]
            public bool? isUsedBySystem { get; set; }
        }

        public class GetAllAppParameterRoot
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
        }
    }
}
