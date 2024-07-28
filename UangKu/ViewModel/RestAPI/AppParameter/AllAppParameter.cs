using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppParameter.GetAllAppParameter;

namespace UangKu.ViewModel.RestAPI.AppParameter
{
    public class AllAppParameter : BaseModel
    {
        private const string AllAppParameterEndPoint = "{2}AppParameter/GetAllAppParameter?PageNumber={0}&PageSize={1}";

        public static async Task<GetAllAppParameterRoot> GetAllAppParameter(int pageNumber, int pageSize)
        {
            GetAllAppParameterRoot root = new GetAllAppParameterRoot();
            string url = string.Format(AllAppParameterEndPoint, pageNumber, pageSize, URL);
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
                    var content = JsonConvert.DeserializeObject<GetAllAppParameterRoot>(response.Content);
                    root = new GetAllAppParameterRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Parameter {response.StatusDescription}"
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
                    root = new GetAllAppParameterRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Parameter {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new GetAllAppParameterRoot
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
