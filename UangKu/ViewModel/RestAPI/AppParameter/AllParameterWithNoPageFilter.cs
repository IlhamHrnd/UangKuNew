using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppParameter.GetAllParameterWithNoPageFilter;

namespace UangKu.ViewModel.RestAPI.AppParameter
{
    public class AllParameterWithNoPageFilter : BaseModel
    {
        private const string AllAppParameterEndPoint = "{0}AppParameter/GetAllParameterWithNoPageFilter";

        public static async Task<ParameterWithNoPageFilterRoot> GetAllAppParameter()
        {
            ParameterWithNoPageFilterRoot root = new ParameterWithNoPageFilterRoot();
            string url = string.Format(AllAppParameterEndPoint, URL);
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
                    var content = JsonConvert.DeserializeObject<List<Datum>>(response.Content);
                    root = new ParameterWithNoPageFilterRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"App Parameter {response.StatusDescription}"
                        },
                        data = content
                    };
                }
                else
                {
                    root = new ParameterWithNoPageFilterRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"App Parameter {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new ParameterWithNoPageFilterRoot
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
