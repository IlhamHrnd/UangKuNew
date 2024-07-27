using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.User
{
    public  class AllUser
    {
        public class Datum
        {
            [JsonProperty("username")]
            public string username { get; set; }

            [JsonProperty("sexName")]
            public string sexName { get; set; }

            [JsonProperty("accessName")]
            public string accessName { get; set; }

            [JsonProperty("statusName")]
            public string statusName { get; set; }

            [JsonProperty("activeDate")]
            public DateTime? activeDate { get; set; }

            [JsonProperty("lastLogin")]
            public DateTime? lastLogin { get; set; }

            [JsonProperty("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonProperty("lastUpdateByUser")]
            public string lastUpdateByUser { get; set; }

            [JsonProperty("personID")]
            public string personID { get; set; }
            public bool? isActive { get; set; }
            public string dateActive { get; set; }
            public string dateLogin { get; set; } 
        }

        public class AllUserRoot : IResponse
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
