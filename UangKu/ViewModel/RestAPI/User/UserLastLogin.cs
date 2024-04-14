using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserLastLogin : BaseModel
    {
        private const string UserLastLoginEndPoint = "{1}User/UpdateLastLogin?username={0}";

        public static async Task<string> PatchUserLastLogin(string username)
        {
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
