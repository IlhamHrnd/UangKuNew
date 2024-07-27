using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Response.User;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserName : BaseModel
    {
        private const string UserNameEndPoint = "{1}User/GetUsername?Username={0}";

        public static async Task<UsernameRoot> GetUserName(string username)
        {
            UsernameRoot root = new UsernameRoot();
            string url = string.Format(UserNameEndPoint, username, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = TimeOut
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var format = response.Content.Substring(1, response.Content.Length - 2); ;
                    var content = JsonConvert.DeserializeObject<UsernameRoot>(format);
                    root = new UsernameRoot
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
                        personID = content.personID,
                        imgavatar = content.imgavatar
                    };
                }
                else
                {
                    root = new UsernameRoot
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
            catch (Exception ex)
            {
                root = new UsernameRoot
                {
                    metaData = new MetaData
                    {
                        code = 201,
                        isSucces = false,
                        message = ex.Message
                    }
                };
            }
            return root;
        }
    }
}
