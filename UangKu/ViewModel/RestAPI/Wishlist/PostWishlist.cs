using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Wishlist
{
    public class PostWishlist : BaseModel
    {
        private const string PostWishlistEndPoint = "{0}UserWishlist/PostUserWishlist";

        public static async Task<string> PostNewWishlist(Model.Index.Body.PostWishlist wishlist)
        {
            string result;
            string url = string.Format(PostWishlistEndPoint, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeOut
            };
            var body = new Model.Index.Body.PostWishlist
            {
                wishlistID = wishlist.wishlistID,
                personID = wishlist.personID,
                srProductCategory = wishlist.srProductCategory,
                productName = wishlist.productName,
                productQuantity = wishlist.productQuantity,
                productPrice = wishlist.productPrice,
                productLink = wishlist.productLink,
                createdByUserID = wishlist.createdByUserID,
                createdDateTime = wishlist.createdDateTime,
                lastUpdateByUserID = wishlist.lastUpdateByUserID,
                lastUpdateDateTime = wishlist.lastUpdateDateTime,
                wishlistDate = wishlist.wishlistDate,
                productPicture = wishlist.productPicture,
                isComplete = wishlist.isComplete,
                pictureSize = wishlist.pictureSize
            };
            request.AddJsonBody(body);

            var response = await client.ExecutePostAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.Substring(1, response.Content.Length - 2);
                }
                else
                {
                    result = response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}
