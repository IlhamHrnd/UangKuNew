using System.Text.Json.Serialization;

namespace UangKu.WebService.Data
{
    public class Transaction
    {
        public class Data
        {
            [JsonPropertyName("transNo")]
            public string transNo { get; set; }

            [JsonPropertyName("personId")]
            public string personId { get; set; }

            [JsonPropertyName("srtransaction")]
            public string srtransaction { get; set; }

            [JsonPropertyName("srtransItem")]
            public string srtransItem { get; set; }

            [JsonPropertyName("amount")]
            public double? amount { get; set; }

            [JsonPropertyName("description")]
            public string description { get; set; }

            [JsonPropertyName("photo")]
            public string photo { get; set; }

            [JsonPropertyName("transType")]
            public string transType { get; set; }

            [JsonPropertyName("transDate")]
            public DateTime? transDate { get; set; }

            [JsonPropertyName("createdDateTime")]
            public DateTime? createdDateTime { get; set; }

            [JsonPropertyName("createdByUserId")]
            public string createdByUserId { get; set; }

            [JsonPropertyName("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonPropertyName("lastUpdateByUserId")]
            public string lastUpdateByUserId { get; set; }

            #region Custom Variabel
            public string amountFormat { get; set; }
            public string dateFormat { get; set; }
            public ImageSource source { get; set; }
            #endregion
        }
    }
}
