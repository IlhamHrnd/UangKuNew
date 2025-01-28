using Newtonsoft.Json;

namespace UangKu.WebService.Data
{
    public class Root<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("succeeded")]
        public bool? Succeeded { get; set; }

        [JsonProperty("errors")]
        public object Errors { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

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
    }
}
