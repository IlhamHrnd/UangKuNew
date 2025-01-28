using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Report
{
    public class PostReport : BaseModel
    {
        private const string ReportEndPoint = "{0}UserReport/PostUserReport";

        public static async Task<string> PostNewReport(Model.Index.Body.PostReport report)
        {
            string result;
            string url = string.Format(ReportEndPoint, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            var body = new Model.Index.Body.PostReport
            {
                reportNo = report.reportNo,
                dateErrorOccured = report.dateErrorOccured,
                srErrorLocation = report.srErrorLocation,
                srErrorPossibility = report.srErrorPossibility,
                errorCronologic = report.errorCronologic,
                picture = report.picture,
                createdDateTime = report.createdDateTime,
                createdByUserID = report.createdByUserID,
                lastUpdateDateTime = report.lastUpdateDateTime,
                lastUpdateByUserID = report.lastUpdateByUserID,
                personID = report.personID
            };
            request.AddJsonBody(body);

            var response = await client.ExecutePostAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.Substring(1, response.Content.Length - 2);
                }
                else
                {
                    result = response.Content;
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}
