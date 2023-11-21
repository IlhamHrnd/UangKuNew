using Newtonsoft.Json;

namespace UangKu.Model.Index.Body
{
    public class PostPicture
    {
        [JsonProperty("pictureID")]
        public string pictureID { get; set; }

        [JsonProperty("picture")]
        public string picture { get; set; }

        [JsonProperty("pictureName")]
        public string pictureName { get; set; }

        [JsonProperty("pictureFormat")]
        public string pictureFormat { get; set; }

        [JsonProperty("personID")]
        public string personID { get; set; }

        [JsonProperty("isDeleted")]
        public bool? isDeleted { get; set; }

        [JsonProperty("createdByUserID")]
        public string createdByUserID { get; set; }

        [JsonProperty("createdDateTime")]
        public DateTime? createdDateTime { get; set; }

        [JsonProperty("lastUpdateDateTime")]
        public DateTime? lastUpdateDateTime { get; set; }

        [JsonProperty("lastUpdateByUserID")]
        public string lastUpdateByUserID { get; set; }
    }
}
