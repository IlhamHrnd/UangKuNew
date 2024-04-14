using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Wishlist
{
    public class PatchWishlist : BaseModel
    {
        private const string PatchWishlistEndPoint = "{0}UserWishlist/PatchUserWishlist";

        public static async Task<string> PatchWishlistID(Model.Index.Body.PatchWishlist wishlist)
        {
            string url = string.Format(PatchWishlistEndPoint, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeOut
            };
            var body = new Model.Index.Body.PatchWishlist
            {
                wishlistID = wishlist.wishlistID,
                srProductCategory = wishlist.srProductCategory,
                productName = wishlist.productName,
                productQuantity = wishlist.productQuantity,
                productPrice = wishlist.productPrice,
                productLink = wishlist.productLink,
                lastUpdateByUserID = wishlist.lastUpdateByUserID,
                lastUpdateDateTime = wishlist.lastUpdateDateTime,
                wishlistDate = wishlist.wishlistDate,
                productPicture = wishlist.productPicture,
                isComplete = wishlist.isComplete,
                pictureSize = wishlist.pictureSize,
            };
            request.AddJsonBody(body);

            var response = await client.ExecuteAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var patch = JsonConvert.DeserializeObject<string>(content);
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
            var format = response.Content.Substring(1, response.Content.Length - 2);

            return format;
        }
    }
}
