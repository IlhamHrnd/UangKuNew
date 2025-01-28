using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Wishlist
{
    public class GetNewWishlistID : BaseModel
    {
        private const string GetNewWishlistIDEndPoint = "{0}UserWishlist/GetNewUserWishlistID?TransType={1}";

        public static async Task<string> GetNewWishlistIDNo(string transType)
        {
            string result;
            string wishlistID = string.Empty;
            string url = string.Format(GetNewWishlistIDEndPoint, URL, transType);
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
