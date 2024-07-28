using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Wishlist.GetUserWishlistPerCategory;

namespace UangKu.ViewModel.RestAPI.Wishlist
{
    public class GetUserWishlistPerCategory : BaseModel
    {
        private const string GetUserWishlistPerCategoryEndPoint = "{0}UserWishlist/GetUserWishlistPerCategory?PersonID={1}&IsComplete={2}";

        public static async Task<GetUserWishlistPerCategoryRoot> UserWishlistPerCategory(string personID, bool isComplete)
        {
            GetUserWishlistPerCategoryRoot root = new GetUserWishlistPerCategoryRoot();
            string url = string.Format(GetUserWishlistPerCategoryEndPoint, URL, personID, isComplete);
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
                    var content = JsonConvert.DeserializeObject<List<Datum>>(response.Content);
                    root = new GetUserWishlistPerCategoryRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Wishlist {response.StatusDescription}"
                        },
                        data = content
                    };
                }
                else
                {
                    root = new GetUserWishlistPerCategoryRoot
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
                root = new GetUserWishlistPerCategoryRoot
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
