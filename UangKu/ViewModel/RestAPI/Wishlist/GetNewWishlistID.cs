using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Wishlist
{
    public class GetNewWishlistID : BaseModel
    {
        private const string GetNewWishlistIDEndPoint = "{0}UserWishlist/GetNewUserWishlistID?TransType={1}";

        public static async Task<string> GetNewWishlistIDNo(string transType)
        {
            string wishlistID = string.Empty;
            string url = string.Format(GetNewWishlistIDEndPoint, SessionModel.APIUrlLink(), transType);
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
                    var get = JsonConvert.DeserializeObject<string>(content);
                    wishlistID = get;
                }
                else
                {
                    await MsgModel.MsgNotification(response.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }

            return wishlistID;
        }
    }
}
