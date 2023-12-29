using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class PostTransaction
    {
        private const string PostTransactionEndPoint = "https://uangkuapi.azurewebsites.net/Transaction/PostTransaction";

        public static async Task<string> PostTransactionTransNo(Model.Index.Body.PostTransaction transaction)
        {
            string url = string.Format(PostTransactionEndPoint);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = ParameterModel.ItemDefaultValue.Timeout
            };
            var body = new Model.Index.Body.PostTransaction
            {
                transNo = transaction.transNo,
                srTransaction = transaction.srTransaction,
                srTransItem = transaction.srTransItem,
                amount = transaction.amount,
                description = transaction.description,
                photo = transaction.photo,
                createdDateTime = transaction.createdDateTime,
                createdByUserID = transaction.createdByUserID,
                lastUpdateDateTime = transaction.lastUpdateDateTime,
                lastUpdateByUserID = transaction.lastUpdateByUserID,
                transType = transaction.transType,
                transDate = transaction.transDate,
                personID = transaction.personID
            };
            request.AddJsonBody(body);

            var response = await client.ExecutePostAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var post = JsonConvert.DeserializeObject<string>(content);
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
            var format = response.Content.Substring(1, response.Content.Length - 2);

            return format;
        }
    }
}