using Newtonsoft.Json;

namespace UangKu.Model.Index.Body
{
    public class PostWishlist
    {
        [JsonProperty("wishlistID")]
        public string wishlistID { get; set; }

        [JsonProperty("personID")]
        public string personID { get; set; }

        [JsonProperty("srProductCategory")]
        public string srProductCategory { get; set; }

        [JsonProperty("productName")]
        public string productName { get; set; }

        [JsonProperty("productQuantity")]
        public int? productQuantity { get; set; }

        [JsonProperty("productPrice")]
        public int? productPrice { get; set; }

        [JsonProperty("productLink")]
        public string productLink { get; set; }

        [JsonProperty("createdByUserID")]
        public string createdByUserID { get; set; }

        [JsonProperty("createdDateTime")]
        public DateTime? createdDateTime { get; set; }

        [JsonProperty("lastUpdateByUserID")]
        public string lastUpdateByUserID { get; set; }

        [JsonProperty("lastUpdateDateTime")]
        public DateTime? lastUpdateDateTime { get; set; }

        [JsonProperty("wishlistDate")]
        public DateTime? wishlistDate { get; set; }

        [JsonProperty("productPicture")]
        public string productPicture { get; set; }

        [JsonProperty("isComplete")]
        public bool? isComplete { get; set; }

        [JsonProperty("pictureSize")]
        public long? pictureSize { get; set; }
    }
}
