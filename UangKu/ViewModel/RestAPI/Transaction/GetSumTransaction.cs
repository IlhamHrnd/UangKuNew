﻿using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Response.Transaction;
using static UangKu.Model.Response.Transaction.SumTransaction;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class GetSumTransaction
    {
        private const string SumTransactionEndPoint = "https://uangkuapi.azurewebsites.net/Transaction/GetSumTransaction?personID={0}{1}";

        public static async Task<List<SumTransactionRoot>> GetSumTransactionID(string personID, string dateRange)
        {
            List<SumTransactionRoot> root = new List<SumTransactionRoot>();
            string url = string.Format(SumTransactionEndPoint, personID, dateRange);
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
                    var get = JsonConvert.DeserializeObject<List<SumTransactionRoot>>(content);
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
