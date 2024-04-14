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
                Timeout = TimeOut
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var format = content.Substring(1, content.Length - 2);
                    var get = JsonConvert.DeserializeObject<GetWishlistIDRoot>(format);
                    root = get;
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }

            return root;
        }
    }
}
