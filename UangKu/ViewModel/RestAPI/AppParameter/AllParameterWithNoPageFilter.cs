using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppParameter.GetAllParameterWithNoPageFilter;

namespace UangKu.ViewModel.RestAPI.AppParameter
{
    public class AllParameterWithNoPageFilter : BaseModel
    {
        private const string AllAppParameterEndPoint = "{0}AppParameter/GetAllParameterWithNoPageFilter";

        public static async Task<List<ParameterWithNoPageFilterRoot>> GetAllAppParameter()
        {
            List<ParameterWithNoPageFilterRoot> root = new List<ParameterWithNoPageFilterRoot>();
            string url = string.Format(AllAppParameterEndPoint, SessionModel.APIUrlLink());
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
