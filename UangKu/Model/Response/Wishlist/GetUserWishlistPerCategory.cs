using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.Wishlist
{
    public class GetUserWishlistPerCategory
    {
        public class GetUserWishlistPerCategoryRoot : IResponse
        {
            public List<Datum> data { get; set; }
            public MetaData metaData { get; set; }
        }

        public class Datum
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
