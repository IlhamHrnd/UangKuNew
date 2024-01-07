using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Response.User;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserLogin
    {
        private const string UserLoginEndPoint = "https://uangkuapi.azurewebsites.net/User/GetLoginUserName?Username={0}&Password={1}";

        public static async Task<UserRoot> GetUsernameLogin(string username, string password)
        {
            UserRoot root = new UserRoot();
            string url = string.Format(UserLoginEndPoint, username, password);
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
