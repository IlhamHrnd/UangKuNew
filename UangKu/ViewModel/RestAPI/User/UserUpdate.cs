using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserUpdate : BaseModel
    {
        private const string UserUpdateEndPoint = "{1}User/UpdateUsername?username={0}";

        public static async Task<string> PatchUsername(Model.Index.Body.PatchUsername user, string username)
        {
            string result;
            string url = string.Format(UserUpdateEndPoint, username, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeOut
            };
            var body = new Model.Index.Body.PatchUsername
            {
                sex = user.sex,
                access = user.access,
                isActive = user.isActive,
                lastUpdateUser = user.lastUpdateUser,
            };
            request.AddJsonBody(body);

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
