using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserLastLogin : BaseModel
    {
        private const string UserLastLoginEndPoint = "{1}User/UpdateLastLogin?username={0}";

        public static async Task<string> PatchUserLastLogin(string username)
        {
            string result;
            string url = string.Format(UserLastLoginEndPoint, username, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeOut
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
