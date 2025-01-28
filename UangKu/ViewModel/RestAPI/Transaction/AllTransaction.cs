using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Transaction.AllTransaction;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class AllTransaction : BaseModel
    {
        private const string AllTransactionEndPoint = "{4}Transaction/GetAllTransaction?PageNumber={0}&PageSize={1}&PersonID={2}{3}";

        public static async Task<AllTransactionRoot> GetAllTransaction(int pageNumber, int pageSize, string personID, string dateRange)
        {
            AllTransactionRoot root = new AllTransactionRoot();
            string url = string.Format(AllTransactionEndPoint, pageNumber, pageSize, personID, dateRange, URL);
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
                    var content = JsonConvert.DeserializeObject<AllTransactionRoot>(response.Content);
                    root = new AllTransactionRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Transaction {response.StatusDescription}"
                        },
                        pageNumber = content.pageNumber,
                        pageSize = content.pageSize,
                        totalPages = content.totalPages,
                        totalRecords = content.totalRecords,
                        prevPageLink = content.prevPageLink,
                        nextPageLink = content.nextPageLink,
                        data = content.data,
                        succeeded = content.succeeded,
                        errors = content.errors,
                        message = content.message
                    };
                }
                else
                {
                    root = new AllTransactionRoot
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
                root = new AllTransactionRoot
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
