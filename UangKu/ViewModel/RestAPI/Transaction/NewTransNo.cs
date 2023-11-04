using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class NewTransNo
    {
        private const string GetNewTransNoEndPoint = "https://uangkuapi.azurewebsites.net/Transaction/GetNewTransactionNo?TransType={0}";

        public static async Task<string> GetNewTransNo(string transType)
        {
            string transNo = string.Empty;
            string url = string.Format(GetNewTransNoEndPoint, transType);
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
                    var get = JsonConvert.DeserializeObject<string>(content);
                    transNo = get;
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

            return transNo;
        }
    }
}
