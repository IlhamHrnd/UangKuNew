﻿using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Response.Transaction;
using static UangKu.Model.Response.Transaction.SumTransaction;

namespace UangKu.ViewModel.RestAPI.Transaction
{
    public class GetSumTransaction : BaseModel
    {
        private const string SumTransactionEndPoint = "{1}Transaction/GetSumTransaction?personID={0}{2}";

        public static async Task<SumTransactionRoot> GetSumTransactionID(string personID, string dateRange)
        {
            SumTransactionRoot root = new SumTransactionRoot();
            string url = string.Format(SumTransactionEndPoint, personID, URL, dateRange);
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
                    var content = JsonConvert.DeserializeObject<List<Datum>>(response.Content);
                    root = new SumTransactionRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Transaction {response.StatusDescription}"
                        },
                        data = content
                    };
                }
                else
                {
                    root = new SumTransactionRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Transaction {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new SumTransactionRoot
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
