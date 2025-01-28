using System.Text.Json.Serialization;

namespace UangKu.WebService.Data
{
    public class UserDocument
    {
        public class Data
        {
            [JsonPropertyName("documentId")]
            public string documentId { get; set; }

            [JsonPropertyName("personId")]
            public string personId { get; set; }

            [JsonPropertyName("fileName")]
            public string fileName { get; set; }

            [JsonPropertyName("fileExtention")]
            public string fileExtention { get; set; }

            [JsonPropertyName("note")]
            public string note { get; set; }

            [JsonPropertyName("documentDate")]
            public DateTime? documentDate { get; set; }

            [JsonPropertyName("filePath")]
            public string filePath { get; set; }

            [JsonPropertyName("isDeleted")]
            public int? isDeleted { get; set; }

            [JsonPropertyName("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonPropertyName("lastUpdateByUserId")]
            public string lastUpdateByUserId { get; set; }

            [JsonPropertyName("createdDateTime")]
            public DateTime? createdDateTime { get; set; }

            [JsonPropertyName("createdByUserId")]
            public string createdByUserId { get; set; }
        }
    }
}
