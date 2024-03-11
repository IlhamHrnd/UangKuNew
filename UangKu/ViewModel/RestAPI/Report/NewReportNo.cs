using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Report
{
    internal class NewReportNo
    {
        private const string GetNewReportNoEndPoint = "{1}UserReport/GetNewReportNo?TransType={0}";

        public static async Task<string> GetNewReportNo(string reportType)
        {
            string reportNo = string.Empty;
            string url = string.Format(GetNewReportNoEndPoint, reportType, SessionModel.APIUrlLink());
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
