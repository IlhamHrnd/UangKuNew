using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Index.Body;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserSignUp
    {
        private const string UserSignUpEndPoint = "https://uangkuapi.azurewebsites.net/User/CreateUsername?password={0}&email={1}";

        public static async Task<string> PostUserSignUp(string username, string password, string sexname, string email)
        {
            DateTime dateTime = DateTime.Now;
            string date = $"{dateTime: yyyy-MM-dd HH:mm:ss}";

            string url = string.Format(UserSignUpEndPoint, password, email);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = ParameterModel.ItemDefaultValue.Timeout
            };
            var body = new SignUpBody
            {
                username = username,
                sexName = sexname,
                activeDate = dateTime,
                lastLogin = dateTime,
                lastUpdateDateTime = dateTime,
                lastUpdateByUser = username,
                personID = username
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
