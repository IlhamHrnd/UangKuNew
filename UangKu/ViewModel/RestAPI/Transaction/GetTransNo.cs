using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Response.Transaction;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class GetTransNo : BaseModel
    {
        private const string GetTransNoEndPoint = "{1}Transaction/GetTransactionNo?TransNo={0}";

        public static async Task<GetTransactionNoRoot> GetTransactionNo(string transNo)
        {
            GetTransactionNoRoot root = new GetTransactionNoRoot();
            string url = string.Format(GetTransNoEndPoint, transNo, URL);
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
                    var format = response.Content.Substring(1, response.Content.Length - 2);
                    var content = JsonConvert.DeserializeObject<GetTransactionNoRoot>(format);
                    root = new GetTransactionNoRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Transaction {response.StatusDescription}"
                        },
                        transNo = content.transNo,
                        srTransaction = content.srTransaction,
                        srTransItem = content.srTransItem,
                        amount = content.amount,
                        description = content.description,
                        photo = content.photo,
                        transType = content.transType,
                        personID = content.personID,
                        transDate = content.transDate
                    };
                }
                else
                {
                    root = new GetTransactionNoRoot
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
                root = new GetTransactionNoRoot
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
