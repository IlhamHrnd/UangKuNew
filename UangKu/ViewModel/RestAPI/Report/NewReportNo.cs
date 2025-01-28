using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.GenerateAutoNumber.AutoNumber;

namespace UangKu.ViewModel.RestAPI.Report
{
    internal class NewReportNo : BaseModel
    {
        private const string GetNewReportNoEndPoint = "{1}UserReport/GetNewReportNo?TransType={0}";

        public static async Task<AutoNumberRoot> GetNewReportNo(string reportType)
        {
            AutoNumberRoot root = new AutoNumberRoot();
            string url = string.Format(GetNewReportNoEndPoint, reportType, URL);
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
                    var content = JsonConvert.DeserializeObject<string>(response.Content);
                    root = new AutoNumberRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Report {response.StatusDescription}"
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
                            message = $"Report {response.StatusDescription}"
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
