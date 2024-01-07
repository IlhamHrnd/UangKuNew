using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Response.Transaction;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class GetTransNo
    {
        private const string GetTransNoEndPoint = "https://uangkuapi.azurewebsites.net/Transaction/GetTransactionNo?TransNo={0}";

        public static async Task<GetTransactionNoRoot> GetTransactionNo(string transNo)
        {
            GetTransactionNoRoot root = new GetTransactionNoRoot();
            string url = string.Format(GetTransNoEndPoint, transNo);
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
                    var format = content.Substring(1, content.Length - 2);
                    var get = JsonConvert.DeserializeObject<GetTransactionNoRoot>(format);
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
