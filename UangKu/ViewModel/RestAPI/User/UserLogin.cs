using Newtonsoft.Json;
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
            string url = string.Format(UserLoginEndPoint, username, password, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var format = response.Content.Substring(1, response.Content.Length - 2);
                    var content = JsonConvert.DeserializeObject<UserRoot>(format);
                    root = new UserRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Username {response.StatusDescription}"
                        },
                        username = content.username,
                        sexName = content.sexName,
                        accessName = content.accessName,
                        statusName = content.statusName,
                        activeDate = content.activeDate,
                        lastLogin = content.lastLogin,
                        lastUpdateDateTime = content.lastUpdateDateTime,
                        lastUpdateByUser = content.lastUpdateByUser,
                        personID = content.personID
                    };
                }
                else
                {
                    root = new UserRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Username {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception e)
            {
                root = new UserRoot
                {
                    metaData = new MetaData
                    {
                        code = 201,
                        isSucces = false,
                        message = $"Username {response.StatusDescription}"
                    }
                };
            }
            return root;
        }
    }
}
