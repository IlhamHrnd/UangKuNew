﻿using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.AppParameter
{
    public class PostAppParameter
    {
        private const string PostAppParameterEndPoint = "https://uangkuapi.azurewebsites.net/AppParameter/PostAppParameter";

        public static async Task<string> PostAppParameterID(Model.Index.Body.PostAppParameter parameter)
        {
            string url = string.Format(PostAppParameterEndPoint);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = Converter.StringToInt(Model.Session.AppParameter.Timeout, ParameterModel.AppParameterDefault.Timeout)
            };
            var body = new Model.Index.Body.PostAppParameter
            {
                parameterID = parameter.parameterID,
                parameterName = parameter.parameterName,
                parameterValue = parameter.parameterValue,
                lastUpdateDateTime = parameter.lastUpdateDateTime,
                lastUpdateByUserID = parameter.lastUpdateByUserID,
                isUsedBySystem = parameter.isUsedBySystem
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
