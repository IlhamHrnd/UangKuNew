using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Location.Subdistrict;

namespace UangKu.ViewModel.RestAPI.Location
{
    public class GetSubdistrict
    {
        private const string GetSubdistrictEndPoint = "https://uangkuapi.azurewebsites.net/Location/GetAllSubDistrict?DistrictID={0}";

        public static async Task<List<SubdistrictRoot>> GetSubdistricts(string districtID)
        {
            List<SubdistrictRoot> root = new List<SubdistrictRoot>();
            string url = string.Format(GetSubdistrictEndPoint, districtID);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = ParameterModel.ItemDefaultValue.Timeout
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var get = JsonConvert.DeserializeObject<List<SubdistrictRoot>>(content);
                    root = get;
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
            return root;
        }
    }
}
