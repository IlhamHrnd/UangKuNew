using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Transaction.GetAllPDFTransaction;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class GetAllPDFTransaction : BaseModel
    {
        private const string GetAllPDFTransactionEndPoint = "{0}Transaction/GetAllPDFTransaction?PersonID={1}{2}";

        public static async Task<List<PDFTransactionRoot>> AllPDFTransaction(string personID, string dateRange)
        {
            List<PDFTransactionRoot> root = new List<PDFTransactionRoot>();
            string url = string.Format(GetAllPDFTransactionEndPoint, URL, personID, dateRange);
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
                    var get = JsonConvert.DeserializeObject<List<PDFTransactionRoot>>(content);
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
