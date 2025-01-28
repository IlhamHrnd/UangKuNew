using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Picture.UserPicture;

namespace UangKu.ViewModel.RestAPI.Picture
{
    public class GetUserPicture : BaseModel
    {
        private const string GetUserPictureEndPoint = "{4}UserPicture/GetUserPicture?PageNumber={0}&PageSize={1}&PersonID={2}&IsDeleted={3}";

        public static async Task<UserPictureRoot> GetAllUserPicture(int pageNumber, int pageSize, string personID, bool isDeleted)
        {
            UserPictureRoot root = new UserPictureRoot();
            string url = string.Format(GetUserPictureEndPoint, pageNumber, pageSize, personID, isDeleted, URL);
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
                    var content = JsonConvert.DeserializeObject<UserPictureRoot>(response.Content);
                    root = new UserPictureRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Transaction {response.StatusDescription}"
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
                    root = new UserPictureRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Picture {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new UserPictureRoot
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
