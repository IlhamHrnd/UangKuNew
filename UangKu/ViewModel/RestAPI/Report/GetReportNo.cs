﻿using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Response.Report;

namespace UangKu.ViewModel.RestAPI.Report
{
    public class GetReportNo
    {
        private const string GetReportNoEndPoint = "https://uangkuapi.azurewebsites.net/UserReport/GetReportNo?ReportNo={0}&IsAdmin={1}";

        public static async Task<GetReportNoRoot> GetUserReportNo(string reportNo, bool isAdmin)
        {
            GetReportNoRoot root = new GetReportNoRoot();
            string url = string.Format(GetReportNoEndPoint, reportNo, isAdmin);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = ParameterModel.ItemDefaultValue.Timeout
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var format = content.Substring(1, content.Length - 2);
                    var get = JsonConvert.DeserializeObject<GetReportNoRoot>(format);
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