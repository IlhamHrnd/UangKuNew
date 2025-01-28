using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReference.AppStandardReference;

namespace UangKu.ViewModel.RestAPI.AppStandardReferenceItem
{
    public class AppStandardReference : BaseModel
    {
        private const string AppStandardReferenceEndPoint = "{2}AppStandardReference/GetAllReferenceID?PageNumber={0}&PageSize={1}";

        public static async Task<AppStandardReferenceRoot> GetAllASR(int pageNumber, int pageSize)
        {
            AppStandardReferenceRoot root = new AppStandardReferenceRoot();
            string url = string.Format(AppStandardReferenceEndPoint, pageNumber, pageSize, URL);
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
                    var content = JsonConvert.DeserializeObject<AppStandardReferenceRoot>(response.Content);
                    root = new AppStandardReferenceRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"App Standard Reference {response.StatusDescription}"
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
                    root = new AppStandardReferenceRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"App Standard Reference {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new AppStandardReferenceRoot
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
