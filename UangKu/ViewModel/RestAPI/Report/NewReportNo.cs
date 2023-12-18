﻿using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Report
{
    internal class NewReportNo
    {
        private const string GetNewReportNoEndPoint = "https://uangkuapi.azurewebsites.net/UserReport/GetNewReportNo?TransType={0}";

        public static async Task<string> GetNewReportNo(string reportType)
        {
            string reportNo = string.Empty;
            string url = string.Format(GetNewReportNoEndPoint, reportType);
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
