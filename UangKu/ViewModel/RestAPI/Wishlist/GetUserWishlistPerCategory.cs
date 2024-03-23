using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Wishlist.GetUserWishlistPerCategory;

namespace UangKu.ViewModel.RestAPI.Wishlist
{
    public class GetUserWishlistPerCategory
    {
        private const string GetUserWishlistPerCategoryEndPoint = "{0}UserWishlist/GetUserWishlistPerCategory?PersonID={1}&IsComplete={2}";

        public static async Task<List<GetUserWishlistPerCategoryRoot>> UserWishlistPerCategory(string personID, bool isComplete)
        {
            List<GetUserWishlistPerCategoryRoot> root = new List<GetUserWishlistPerCategoryRoot>();
            string url = string.Format(GetUserWishlistPerCategoryEndPoint, SessionModel.APIUrlLink(), personID, isComplete);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = Converter.StringToInt(Model.Session.AppParameter.Timeout, ParameterModel.AppParameterDefault.Timeout)
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var get = JsonConvert.DeserializeObject<List<GetUserWishlistPerCategoryRoot>>(content);
                    root = get;
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
            return root;
        }
    }
}
