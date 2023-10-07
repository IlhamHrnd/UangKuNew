using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserForgotPassword
    {
        private const string UserForgotPasswordEndPoint = "https://uangkuapi.azurewebsites.net/User/UpdatePasswordUser?username={0}&password={1}&email={2}";

        public static async Task<string> PatchUserForgotPassword(string username, string email, string password)
        {
            string url = string.Format(UserForgotPasswordEndPoint, username, password, email);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = ParameterModel.ItemDefaultValue.Timeout
            };
            var response = await client.ExecuteAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var patch = JsonConvert.DeserializeObject<string>(content);
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
