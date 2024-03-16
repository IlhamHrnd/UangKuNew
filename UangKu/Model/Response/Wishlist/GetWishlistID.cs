using Newtonsoft.Json;

namespace UangKu.Model.Response.Wishlist
{
    public class GetWishlistIDRoot
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
        public decimal? productPrice { get; set; }

        [JsonProperty("productLink")]
        public string productLink { get; set; }

        [JsonProperty("lastUpdateDateTime")]
        public DateTime? lastUpdateDateTime { get; set; }

        [JsonProperty("wishlistDate")]
        public DateTime? wishlistDate { get; set; }

        [JsonProperty("productPicture")]
        public string productPicture { get; set; }

        [JsonProperty("isComplete")]
        public bool? isComplete { get; set; }
        public string priceFormat { get; set; }
        public string wishlistDateFormat { get; set; }
        public ImageSource source { get; set; }
    }
}
