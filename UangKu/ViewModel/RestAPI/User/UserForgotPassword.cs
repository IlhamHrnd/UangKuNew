using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserForgotPassword : BaseModel
    {
        private const string UserForgotPasswordEndPoint = "{3}User/UpdatePasswordUser?username={0}&password={1}&email={2}";

        public static async Task<string> PatchUserForgotPassword(string username, string email, string password)
        {
            string result;
            string url = string.Format(UserForgotPasswordEndPoint, username, password, email, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            var response = await client.ExecuteAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.Substring(1, response.Content.Length - 2);
                }
                else
                {
                    result = response.Content;
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}
