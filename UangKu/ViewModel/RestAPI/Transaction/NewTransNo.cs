using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.GenerateAutoNumber.AutoNumber;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class NewTransNo : BaseModel
    {
        private const string GetNewTransNoEndPoint = "{1}Transaction/GetNewTransactionNo?TransType={0}";

        public static async Task<AutoNumberRoot> GetNewTransNo(string transType)
        {
            AutoNumberRoot root = new AutoNumberRoot();
            string url = string.Format(GetNewTransNoEndPoint, transType, URL);
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
                    var content = JsonConvert.DeserializeObject<string>(response.Content);
                    root = new AutoNumberRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Transaction {response.StatusDescription}"
                        },
                        AutoNumber = content
                    };
                }
                else
                {
                    root = new AutoNumberRoot
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
                root = new AutoNumberRoot
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
