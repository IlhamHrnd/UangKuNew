using System.Text.Json.Serialization;

namespace UangKu.WebService.Data
{
    public class UserReport
    {
        public class Data
        {
            [JsonPropertyName("reportNo")]
            public string reportNo { get; set; }

            [JsonPropertyName("dateErrorOccured")]
            public DateTime? dateErrorOccured { get; set; }

            [JsonPropertyName("srerrorLocation")]
            public string srerrorLocation { get; set; }

            [JsonPropertyName("srerrorPossibility")]
            public string srerrorPossibility { get; set; }

            [JsonPropertyName("errorCronologic")]
            public string errorCronologic { get; set; }

            [JsonPropertyName("picture")]
            public string picture { get; set; }

            [JsonPropertyName("isApprove")]
            public bool? isApprove { get; set; }

            [JsonPropertyName("srreportStatus")]
            public string srreportStatus { get; set; }

            [JsonPropertyName("approvedDateTime")]
            public DateTime? approvedDateTime { get; set; }

            [JsonPropertyName("approvedByUserId")]
            public string approvedByUserId { get; set; }

            [JsonPropertyName("voidDateTime")]
            public DateTime? voidDateTime { get; set; }

            [JsonPropertyName("voidByUserId")]
            public string voidByUserId { get; set; }

            [JsonPropertyName("createdDateTime")]
            public DateTime? createdDateTime { get; set; }

            [JsonPropertyName("createdByUserId")]
            public string createdByUserId { get; set; }

            [JsonPropertyName("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonPropertyName("lastUpdateByUserId")]
            public string lastUpdateByUserId { get; set; }

            [JsonPropertyName("personId")]
            public string personId { get; set; }
        }
    }
}
