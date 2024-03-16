using Newtonsoft.Json;

namespace UangKu.Model.Response.Wishlist
{
    public class GetAllUserWishlist
    {
        public class Datum
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

        public class GetAllUserWishlistRoot
        {
            [JsonProperty("pageNumber")]
            public int? pageNumber { get; set; }

            [JsonProperty("pageSize")]
            public int? pageSize { get; set; }

            [JsonProperty("totalPages")]
            public int? totalPages { get; set; }

            [JsonProperty("totalRecords")]
            public int? totalRecords { get; set; }

            [JsonProperty("prevPageLink")]
            public object prevPageLink { get; set; }

            [JsonProperty("nextPageLink")]
            public string nextPageLink { get; set; }

            [JsonProperty("data")]
            public List<Datum> data { get; set; }

            [JsonProperty("succeeded")]
            public bool? succeeded { get; set; }

            [JsonProperty("errors")]
            public object errors { get; set; }

            [JsonProperty("message")]
            public object message { get; set; }
        }

    }
}
