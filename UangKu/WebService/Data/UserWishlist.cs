using System.Text.Json.Serialization;

namespace UangKu.WebService.Data
{
    public class UserWishlist
    {
        public class Data
        {
            [JsonPropertyName("wishlistId")]
            public string wishlistId { get; set; }

            [JsonPropertyName("personId")]
            public string personId { get; set; }

            [JsonPropertyName("srproductCategory")]
            public string srproductCategory { get; set; }

            [JsonPropertyName("productName")]
            public string productName { get; set; }

            [JsonPropertyName("productQuantity")]
            public int? productQuantity { get; set; }

            [JsonPropertyName("productPrice")]
            public double? productPrice { get; set; }

            [JsonPropertyName("productLink")]
            public string productLink { get; set; }

            [JsonPropertyName("createdByUserId")]
            public string createdByUserId { get; set; }

            [JsonPropertyName("createdDateTime")]
            public DateTime? createdDateTime { get; set; }

            [JsonPropertyName("lastUpdateByUserId")]
            public string lastUpdateByUserId { get; set; }

            [JsonPropertyName("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonPropertyName("wishlistDate")]
            public string wishlistDate { get; set; }

            [JsonPropertyName("productPicture")]
            public string productPicture { get; set; }

            [JsonPropertyName("isComplete")]
            public int? isComplete { get; set; }

            #region Custom Variabel
            public ImageSource source { get; set; }
            public string priceFormat { get; set; }
            #endregion
        }
    }
}
