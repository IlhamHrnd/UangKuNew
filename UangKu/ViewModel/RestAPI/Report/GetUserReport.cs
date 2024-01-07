using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Report.Report;

namespace UangKu.ViewModel.RestAPI.Report
{
    public class GetUserReport
    {
        private const string UserReportEndPoint = "https://uangkuapi.azurewebsites.net/UserReport/GetUserReport?PageNumber={0}&PageSize={1}{2}";

        public static async Task<ReportRoot> GetAllUserReport(int pageNumber, int pageSize, string personID)
        {
            ReportRoot root = new ReportRoot();
            string url = string.Format(UserReportEndPoint, pageNumber, pageSize, personID);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = Converter.StringToInt(Model.Session.AppParameter.Timeout, ParameterModel.AppParameterDefault.Timeout)
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
