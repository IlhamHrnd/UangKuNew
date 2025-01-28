using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Response.Report;

namespace UangKu.ViewModel.RestAPI.Report
{
    public class GetReportNo : BaseModel
    {
        private const string GetReportNoEndPoint = "{2}UserReport/GetReportNo?ReportNo={0}&IsAdmin={1}";

        public static async Task<GetReportNoRoot> GetUserReportNo(string reportNo, bool isAdmin)
        {
            GetReportNoRoot root = new GetReportNoRoot();
            string url = string.Format(GetReportNoEndPoint, reportNo, isAdmin, URL);
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
                    var format = response.Content.Substring(1, response.Content.Length - 2);
                    var content = JsonConvert.DeserializeObject<GetReportNoRoot>(format);
                    root = new GetReportNoRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Report {response.StatusDescription}"
                        },
                        reportNo = content.reportNo,
                        dateErrorOccured = content.dateErrorOccured,
                        srErrorLocation = content.srErrorLocation,
                        srErrorPossibility = content.srErrorPossibility,
                        errorCronologic = content.errorCronologic,
                        picture = content.picture,
                        isApprove = content.isApprove,
                        srReportStatus = content.srReportStatus,
                        approvedDateTime = content.approvedDateTime,
                        approvedByUserID = content.approvedByUserID,
                        voidDateTime = content.voidDateTime,
                        voidByUserID = content.voidByUserID,
                        createdDateTime = content.createdDateTime,
                        createdByUserID = content.createdByUserID,
                        lastUpdateDateTime = content.lastUpdateDateTime,
                        lastUpdateByUserID = content.lastUpdateByUserID,
                        personID = content.personID
                    };
                }
                else
                {
                    root = new GetReportNoRoot
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
                root = new GetReportNoRoot
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