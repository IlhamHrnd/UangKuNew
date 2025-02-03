using System.Text.Json.Serialization;

namespace UangKu.WebService.Data
{
    public class UserPicture
    {
        public class Data
        {
            [JsonPropertyName("pictureId")]
            public string pictureId { get; set; }

            [JsonPropertyName("picture")]
            public string picture { get; set; }

            [JsonPropertyName("pictureName")]
            public string pictureName { get; set; }

            [JsonPropertyName("pictureFormat")]
            public string pictureFormat { get; set; }

            [JsonPropertyName("personId")]
            public string personId { get; set; }

            [JsonPropertyName("isDeleted")]
            public bool? isDeleted { get; set; }

            [JsonPropertyName("createdByUserId")]
            public string createdByUserId { get; set; }

            [JsonPropertyName("createdDateTime")]
            public DateTime? createdDateTime { get; set; }

            [JsonPropertyName("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonPropertyName("lastUpdateByUserId")]
            public string lastUpdateByUserId { get; set; }

            #region Custom Variabel
            public ImageSource source { get; set; }
            public string contentType { get; set; }
            #endregion
        }
    }
}
