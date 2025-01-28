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
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = JsonConvert.DeserializeObject<GetAllUserWishlistRoot>(response.Content);
                    root = new GetAllUserWishlistRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Wishlist {response.StatusDescription}"
                        },
                        pageNumber = content.pageNumber,
                        pageSize = content.pageSize,
                        totalPages = content.totalPages,
                        totalRecords = content.totalRecords,
                        prevPageLink = content.prevPageLink,
                        nextPageLink = content.nextPageLink,
                        data = content.data,
                        succeeded = content.succeeded,
                        errors = content.errors,
                        message = content.message
                    };
                }
                else
                {
                    root = new GetAllUserWishlistRoot
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
                root = new GetAllUserWishlistRoot
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
