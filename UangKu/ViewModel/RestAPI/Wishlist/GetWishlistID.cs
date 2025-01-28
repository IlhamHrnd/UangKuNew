using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Response.Wishlist;

namespace UangKu.ViewModel.RestAPI.Wishlist
{
    public class GetWishlistID : BaseModel
    {
        private const string GetWishlistIDEndPoint = "{0}UserWishlist/GetUserWishlistID?WishlistID={1}";

        public static async Task<GetWishlistIDRoot> GetWishlistIDUser(string wishlistID)
        {
            GetWishlistIDRoot root = new GetWishlistIDRoot();
            string url = string.Format(GetWishlistIDEndPoint, URL, wishlistID);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var format = response.Content.Substring(1, response.Content.Length - 2);
                    var content = JsonConvert.DeserializeObject<GetWishlistIDRoot>(format);
                    root = new GetWishlistIDRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Wishlist {response.StatusDescription}"
                        },
                        wishlistID = content.wishlistID,
                        personID = content.personID,
                        srProductCategory = content.srProductCategory,
                        productName = content.productName,
                        productQuantity = content.productQuantity,
                        productPrice = content.productPrice,
                        productLink = content.productLink,
                        lastUpdateDateTime = content.lastUpdateDateTime,
                        wishlistDate = content.wishlistDate,
                        productPicture = content.productPicture,
                        isComplete = content.isComplete,
                        priceFormat = content.priceFormat,
                        wishlistDateFormat = content.wishlistDateFormat,
                        source = content.source
                    };
                }
                else
                {
                    root = new GetWishlistIDRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Wishlist {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new GetWishlistIDRoot
                {
                    metaData = new MetaData
                    {
                        code = 201,
                        isSucces = false,
                        message = ex.Message
                    }
                };
            }

            return root;
        }
    }
}
