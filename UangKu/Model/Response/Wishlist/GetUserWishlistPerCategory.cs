using Newtonsoft.Json;

namespace UangKu.Model.Response.Wishlist
{
    public class GetUserWishlistPerCategory
    {
        public class GetUserWishlistPerCategoryRoot
        {
            [JsonProperty("countProductCategory")]
            public int? countProductCategory { get; set; }

            [JsonProperty("itemName")]
            public string itemName { get; set; }

            [JsonProperty("itemIcon")]
            public string itemIcon { get; set; }
            public ImageSource source { get; set; }
        }
    }
}
