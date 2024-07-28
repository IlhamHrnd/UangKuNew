using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Location.District;

namespace UangKu.ViewModel.RestAPI.Location
{
    public class GetDistrict : BaseModel
    {
        private const string GetDistrictEndPoint = "{1}Location/GetAllDistrict?CityID={0}";

        public static async Task<DistrictRoot> GetDistricts(string cityID)
        {
            DistrictRoot root = new DistrictRoot();
            string url = string.Format(GetDistrictEndPoint, cityID, URL);
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
                    root = new DistrictRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"District {response.StatusDescription}"
                        },
                        data = content
                    };
                }
                else
                {
                    root = new DistrictRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"District {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new DistrictRoot
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
