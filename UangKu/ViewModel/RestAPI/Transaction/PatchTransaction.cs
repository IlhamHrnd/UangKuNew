using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class PatchTransaction
    {
        private const string PatchTransactionEndPoint = "https://uangkuapi.azurewebsites.net/Transaction/PatchTransaction";

        public static async Task<string> PatchTransactionTransNo(Model.Index.Body.PatchTransaction transaction)
        {
            string url = string.Format(PatchTransactionEndPoint);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = ParameterModel.ItemDefaultValue.Timeout
            };
            var body = new Model.Index.Body.PatchTransaction
            {
                transNo = transaction.transNo,
                srTransaction = transaction.srTransaction,
                srTransItem = transaction.srTransItem,
                amount = transaction.amount,
                description = transaction.description,
                photo = transaction.photo,
                lastUpdateDateTime = transaction.lastUpdateDateTime,
                lastUpdateByUserID = transaction.lastUpdateByUserID,
                transType = transaction.transType,
                transDate = transaction.transDate
            };
            request.AddJsonBody(body);

            var response = await client.ExecuteAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var patch = JsonConvert.DeserializeObject<string>(content);
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
            var format = response.Content.Substring(1, response.Content.Length - 2);

            return format;
        }
    }
}
