using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Wishlist.GetAllUserWishlist;

namespace UangKu.ViewModel.RestAPI.Wishlist
{
    public class GetAllUserWishlist : BaseModel
    {
        private const string GetAllUserWishlistEndPoint = "{0}UserWishlist/GetAllUserWishlist?PageNumber={2}&PageSize={3}&PersonID={1}";

        public static async Task<GetAllUserWishlistRoot> GetAllWishlist(string personID, int pageNumber, int pageSize)
        {
            GetAllUserWishlistRoot root = new GetAllUserWishlistRoot();
            string url = string.Format(GetAllUserWishlistEndPoint, URL, personID, pageNumber, pageSize);
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
                    var get = JsonConvert.DeserializeObject<GetAllUserWishlistRoot>(content);
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
