using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Report
{
    internal class NewReportNo : BaseModel
    {
        private const string GetNewReportNoEndPoint = "{1}UserReport/GetNewReportNo?TransType={0}";

        public static async Task<string> GetNewReportNo(string reportType)
        {
            string reportNo = string.Empty;
            string url = string.Format(GetNewReportNoEndPoint, reportType, URL);
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
                    var get = JsonConvert.DeserializeObject<string>(content);
                    reportNo = get;
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

            return reportNo;
        }
    }
}
