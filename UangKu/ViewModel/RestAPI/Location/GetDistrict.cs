using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Location.District;

namespace UangKu.ViewModel.RestAPI.Location
{
    public class GetDistrict
    {
        private const string GetDistrictEndPoint = "https://uangkuapi.azurewebsites.net/Location/GetAllDistrict?CityID={0}";

        public static async Task<List<DistrictRoot>> GetDistricts(string cityID)
        {
            List<DistrictRoot> root = new List<DistrictRoot>();
            string url = string.Format(GetDistrictEndPoint, cityID);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = Converter.StringToInt(Model.Session.AppParameter.Timeout, ParameterModel.AppParameterDefault.Timeout)
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var get = JsonConvert.DeserializeObject<List<DistrictRoot>>(content);
                    root = get;
                }
                else
                {
                    await MsgModel.MsgNotification(response.ErrorMessage);
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
