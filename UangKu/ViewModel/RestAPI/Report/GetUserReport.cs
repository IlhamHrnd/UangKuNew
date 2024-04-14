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
                Timeout = TimeOut
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var get = JsonConvert.DeserializeObject<ReportRoot>(content);
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
