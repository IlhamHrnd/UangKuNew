using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Transaction.AllTransaction;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class AllTransaction
    {
        private const string AllTransactionEndPoint = "https://uangkuapi.azurewebsites.net/Transaction/GetAllTransaction?PageNumber={0}&PageSize={1}&PersonID={2}";

        public static async Task<AllTransactionRoot> GetAllTransaction(int pageNumber, int pageSize, string personID)
        {
            AllTransactionRoot root = new AllTransactionRoot();
            string url = string.Format(AllTransactionEndPoint, pageNumber, pageSize, personID);
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
                    var get = JsonConvert.DeserializeObject<AllTransactionRoot>(content);
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
