using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Index.Body;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserSignUp
    {
        private const string UserSignUpEndPoint = "{2}User/CreateUsername?password={0}&email={1}";

        public static async Task<string> PostUserSignUp(string username, string password, string sexname, string email)
        {
            string url = string.Format(UserSignUpEndPoint, password, email, SessionModel.APIUrlLink());
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = Converter.StringToInt(Model.Session.AppParameter.Timeout, ParameterModel.AppParameterDefault.Timeout)
            };
            var body = new SignUpBody
            {
                username = username,
                sexName = sexname,
                activeDate = ParameterModel.DateFormat.DateTime,
                lastLogin = ParameterModel.DateFormat.DateTime,
                lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
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
