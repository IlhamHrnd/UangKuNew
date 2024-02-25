using System.Text.Json.Serialization;

namespace UangKu.Model.Response.Transaction
{
    public class AllTransaction
    {
        public class Datum
        {
            [JsonPropertyName("transNo")]
            public string transNo { get; set; }

            [JsonPropertyName("srTransaction")]
            public string srTransaction { get; set; }

            [JsonPropertyName("srTransItem")]
            public string srTransItem { get; set; }

            [JsonPropertyName("amount")]
            public decimal? amount { get; set; }

            [JsonPropertyName("description")]
            public string description { get; set; }

            [JsonPropertyName("photo")]
            public string photo { get; set; }

            [JsonPropertyName("transType")]
            public string transType { get; set; }

            [JsonPropertyName("personID")]
            public string personID { get; set; }

            [JsonPropertyName("transDate")]
            public DateTime? transDate { get; set; }
            public string amountFormat { get; set; }
            public string transDateFormat { get; set; }
            public ImageSource source { get; set; }
        }

        public class AllTransactionRoot
        {
            [JsonPropertyName("pageNumber")]
            public int? pageNumber { get; set; }

            [JsonPropertyName("pageSize")]
            public int? pageSize { get; set; }

            [JsonPropertyName("totalPages")]
            public int? totalPages { get; set; }

            [JsonPropertyName("totalRecords")]
            public int? totalRecords { get; set; }

            [JsonPropertyName("prevPageLink")]
            public object prevPageLink { get; set; }

            [JsonPropertyName("nextPageLink")]
            public object nextPageLink { get; set; }

            [JsonPropertyName("data")]
            public List<Datum> data { get; set; }

            [JsonPropertyName("succeeded")]
            public bool? succeeded { get; set; }

            [JsonPropertyName("errors")]
            public object errors { get; set; }

            [JsonPropertyName("message")]
            public object message { get; set; }
        }
    }
}
