using System.Text.Json.Serialization;

namespace UangKu.WebService.Data
{
    public class AppStandardReferenceItem
    {
        public class Data
        {
            [JsonPropertyName("standardReferenceId")]
            public string standardReferenceId { get; set; }

            [JsonPropertyName("itemId")]
            public string itemId { get; set; }

            [JsonPropertyName("itemName")]
            public string itemName { get; set; }

            [JsonPropertyName("note")]
            public string note { get; set; }

            [JsonPropertyName("isUsedBySystem")]
            public bool? isUsedBySystem { get; set; }

            [JsonPropertyName("isActive")]
            public bool? isActive { get; set; }

            [JsonPropertyName("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonPropertyName("lastUpdateByUserId")]
            public string lastUpdateByUserId { get; set; }

            [JsonPropertyName("itemIcon")]
            public string itemIcon { get; set; }
        }
    }
}
