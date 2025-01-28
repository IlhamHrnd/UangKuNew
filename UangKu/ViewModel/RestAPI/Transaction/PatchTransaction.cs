using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class PatchTransaction : BaseModel
    {
        private const string PatchTransactionEndPoint = "{0}Transaction/PatchTransaction";

        public static async Task<string> PatchTransactionTransNo(Model.Index.Body.PatchTransaction transaction)
        {
            string result;
            string url = string.Format(PatchTransactionEndPoint, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
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
                    result = response.Content.Substring(1, response.Content.Length - 2);
                }
                else
                {
                    result = response.Content;
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}
