using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Transaction.GetAllPDFTransaction;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class GetAllPDFTransaction : BaseModel
    {
        private const string GetAllPDFTransactionEndPoint = "{0}Transaction/GetAllPDFTransaction?PersonID={1}{2}";

        public static async Task<PDFTransactionRoot> AllPDFTransaction(string personID, string dateRange)
        {
            PDFTransactionRoot root = new PDFTransactionRoot();
            string url = string.Format(GetAllPDFTransactionEndPoint, URL, personID, dateRange);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = JsonConvert.DeserializeObject<List<Datum>>(response.Content);
                    root = new PDFTransactionRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Transaction {response.StatusDescription}"
                        },
                        data = content
                    };
                }
                else
                {
                    root = new PDFTransactionRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Transaction {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new PDFTransactionRoot
                {
                    metaData = new MetaData
                    {
                        code = 201,
                        isSucces = false,
                        message = ex.Message
                    }
                };
            }
            return root;
        }
    }
}
