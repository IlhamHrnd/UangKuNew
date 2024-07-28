using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Location.Cities;

namespace UangKu.ViewModel.RestAPI.Location
{
    public class GetCity : BaseModel
    {
        private const string GetCitiesEndPoint = "{1}Location/GetAllCities?ProvID={0}";

        public static async Task<CitiesRoot> GetCities(string provID)
        {
            CitiesRoot root = new CitiesRoot();
            string url = string.Format(GetCitiesEndPoint, provID, URL);
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
                    root = new CitiesRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Cities {response.StatusDescription}"
                        },
                        data = content
                    };
                }
                else
                {
                    root = new CitiesRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Cities {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new CitiesRoot
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
