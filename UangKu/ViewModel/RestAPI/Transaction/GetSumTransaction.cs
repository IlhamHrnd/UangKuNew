using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Response.Transaction;
using static UangKu.Model.Response.Transaction.SumTransaction;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class GetSumTransaction : BaseModel
    {
        private const string SumTransactionEndPoint = "{1}Transaction/GetSumTransaction?personID={0}{2}";

        public static async Task<List<SumTransactionRoot>> GetSumTransactionID(string personID, string dateRange)
        {
            List<SumTransactionRoot> root = new List<SumTransactionRoot>();
            string url = string.Format(SumTransactionEndPoint, personID, URL, dateRange);
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
                    var get = JsonConvert.DeserializeObject<List<SumTransactionRoot>>(content);
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
