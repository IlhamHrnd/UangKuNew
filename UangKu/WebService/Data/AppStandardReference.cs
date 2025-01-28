using System.Text.Json.Serialization;

namespace UangKu.WebService.Data
{
    public class AppStandardReference
    {
        public class Data
        {
            [JsonPropertyName("standardReferenceId")]
            public string standardReferenceId { get; set; }

            [JsonPropertyName("standardReferenceName")]
            public string standardReferenceName { get; set; }

            [JsonPropertyName("itemLength")]
            public int? itemLength { get; set; }

            [JsonPropertyName("isUsedBySystem")]
            public bool? isUsedBySystem { get; set; }

            [JsonPropertyName("isActive")]
            public bool? isActive { get; set; }

            [JsonPropertyName("note")]
            public string note { get; set; }

            [JsonPropertyName("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonPropertyName("lastUpdateByUserId")]
            public string lastUpdateByUserId { get; set; }
        }
    }
}
