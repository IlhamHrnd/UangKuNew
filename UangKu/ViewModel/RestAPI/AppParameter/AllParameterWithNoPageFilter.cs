using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppParameter.GetAllParameterWithNoPageFilter;

namespace UangKu.ViewModel.RestAPI.AppParameter
{
    public class AllParameterWithNoPageFilter
    {
        private const string AllAppParameterEndPoint = "https://uangkuapi.azurewebsites.net/AppParameter/GetAllParameterWithNoPageFilter";

        public static async Task<List<ParameterWithNoPageFilterRoot>> GetAllAppParameter()
        {
            List<ParameterWithNoPageFilterRoot> root = new List<ParameterWithNoPageFilterRoot>();
            string url = string.Format(AllAppParameterEndPoint);
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
                    var get = JsonConvert.DeserializeObject<List<ParameterWithNoPageFilterRoot>>(content);
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
