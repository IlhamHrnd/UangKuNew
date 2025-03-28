﻿using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.AppStandardReferenceItem
{
    public class AppStandardReferenceItem : BaseModel
    {
        private const string AppStandardReferenceItemEndPoint = "{3}AppStandardReferenceItem/GetStandardID?StandardReferenceID={0}&isActive={1}&isUse={2}";

        public static async Task<List<T>> GetAsriAsync<T>(string id, bool isActive, bool isUse)
        {
            List<T> root = new List<T>();
            string url = string.Format(AppStandardReferenceItemEndPoint, id, isActive, isUse, URL);
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
                    var get = JsonConvert.DeserializeObject<List<T>>(content);
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
