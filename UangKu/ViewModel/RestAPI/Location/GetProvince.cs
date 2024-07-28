using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Location.Provinces;

namespace UangKu.ViewModel.RestAPI.Location
{
    public class GetProvince : BaseModel
    {
        private const string GetProvinceEndPoint = "{0}Location/GetAllProvince";

        public static async Task<ProvincesRoot> GetProvinces()
        {
            ProvincesRoot root = new ProvincesRoot();
            string url = string.Format(GetProvinceEndPoint, URL);
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
                    root = new ProvincesRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Province {response.StatusDescription}"
                        },
                        data = content
                    };
                }
                else
                {
                    root = new ProvincesRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Province {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new ProvincesRoot
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
