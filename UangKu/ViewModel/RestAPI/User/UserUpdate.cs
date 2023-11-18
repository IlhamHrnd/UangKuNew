using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserUpdate
    {
        private const string UserUpdateEndPoint = "https://uangkuapi.azurewebsites.net/User/UpdateUsername?username={0}";

        public static async Task<string> PatchUsername(Model.Index.Body.PatchUsername user, string username)
        {
            string url = string.Format(UserUpdateEndPoint, username);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = ParameterModel.ItemDefaultValue.Timeout
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
                    var content = response.Content;
                    var patch = JsonConvert.DeserializeObject<string>(content);
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
