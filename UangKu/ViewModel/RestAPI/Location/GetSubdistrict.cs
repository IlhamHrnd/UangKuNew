using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Location.Subdistrict;

namespace UangKu.ViewModel.RestAPI.Location
{
    public class GetSubdistrict : BaseModel
    {
        private const string GetSubdistrictEndPoint = "{1}Location/GetAllSubDistrict?DistrictID={0}";

        public static async Task<SubdistrictRoot> GetSubdistricts(string districtID)
        {
            SubdistrictRoot root = new SubdistrictRoot();
            string url = string.Format(GetSubdistrictEndPoint, districtID, URL);
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
                    root = new SubdistrictRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Subdistrict {response.StatusDescription}"
                        },
                        data = content
                    };
                }
                else
                {
                    root = new SubdistrictRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Subdistrict {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new SubdistrictRoot
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
