using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Location.Provinces;

namespace UangKu.ViewModel.RestAPI.Location
{
    public class GetProvince
    {
        private const string GetProvinceEndPoint = "https://uangkuapi.azurewebsites.net/Location/GetAllProvince";

        public static async Task<List<ProvincesRoot>> GetProvinces()
        {
            List<ProvincesRoot> root = new List<ProvincesRoot>();
            string url = string.Format(GetProvinceEndPoint);
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
                    var get = JsonConvert.DeserializeObject<List<ProvincesRoot>>(content);
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
