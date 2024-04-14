using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Report
{
    public class PostReport : BaseModel
    {
        private const string ReportEndPoint = "{0}UserReport/PostUserReport";

        public static async Task<string> PostNewReport(Model.Index.Body.PostReport report)
        {
            string url = string.Format(ReportEndPoint, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeOut
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
                    var content = response.Content;
                    var post = JsonConvert.DeserializeObject<string>(content);
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
            var format = response.Content.Substring(1, response.Content.Length - 2);

            return format;
        }
    }
}
