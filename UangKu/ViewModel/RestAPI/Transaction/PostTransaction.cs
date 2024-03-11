using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class PostTransaction
    {
        private const string PostTransactionEndPoint = "{0}Transaction/PostTransaction";

        public static async Task<string> PostTransactionTransNo(Model.Index.Body.PostTransaction transaction)
        {
            string url = string.Format(PostTransactionEndPoint, SessionModel.APIUrlLink());
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = Converter.StringToInt(Model.Session.AppParameter.Timeout, ParameterModel.AppParameterDefault.Timeout)
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