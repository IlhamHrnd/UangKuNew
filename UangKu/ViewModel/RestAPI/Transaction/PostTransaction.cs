using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class PostTransaction : BaseModel
    {
        private const string PostTransactionEndPoint = "{0}Transaction/PostTransaction";

        public static async Task<string> PostTransactionTransNo(Model.Index.Body.PostTransaction transaction)
        {
            string result;
            string url = string.Format(PostTransactionEndPoint, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeOut
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