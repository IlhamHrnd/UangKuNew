using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Location.Cities;

namespace UangKu.ViewModel.RestAPI.Location
{
    public class GetCity
    {
        private const string GetCitiesEndPoint = "{1}Location/GetAllCities?ProvID={0}";

        public static async Task<List<CitiesRoot>> GetCities(string provID)
        {
            List<CitiesRoot> root = new List<CitiesRoot>();
            string url = string.Format(GetCitiesEndPoint, provID, SessionModel.APIUrlLink());
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
                    var get = JsonConvert.DeserializeObject<List<CitiesRoot>>(content);
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
