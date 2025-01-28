using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Report.Report;

namespace UangKu.ViewModel.RestAPI.Report
{
    public class GetUserReport : BaseModel
    {
        private const string UserReportEndPoint = "{3}UserReport/GetUserReport?PageNumber={0}&PageSize={1}{2}";

        public static async Task<ReportRoot> GetAllUserReport(int pageNumber, int pageSize, string personID)
        {
            ReportRoot root = new ReportRoot();
            string url = string.Format(UserReportEndPoint, pageNumber, pageSize, personID, URL);
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
                    var content = JsonConvert.DeserializeObject<ReportRoot>(response.Content);
                    root = new ReportRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Report {response.StatusDescription}"
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
                    root = new ReportRoot
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
                root = new ReportRoot
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
