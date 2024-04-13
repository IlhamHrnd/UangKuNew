﻿using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Response.User;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserLogin : BaseModel
    {
        private const string UserLoginEndPoint = "{2}User/GetLoginUserName?Username={0}&Password={1}";

        public static async Task<UserRoot> GetUsernameLogin(string username, string password)
        {
            UserRoot root = new UserRoot();
            string url = string.Format(UserLoginEndPoint, username, password, SessionModel.APIUrlLink());
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
                    var format = content.Substring(1, content.Length - 2);
                    var get = JsonConvert.DeserializeObject<UserRoot>(format);
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
