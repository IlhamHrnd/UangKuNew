using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.User.AllUser;

namespace UangKu.ViewModel.RestAPI.User
{
    public class UserAll
    {
        private const string AllUserEndPoint = "https://uangkuapi.azurewebsites.net/User/GetAllUser?PageNumber={0}&PageSize={1}";

        public static async Task<AllUserRoot> GetAllUser(int pageNumber, int pageSize)
        {
            AllUserRoot root = new AllUserRoot();
            string url = string.Format(AllUserEndPoint, pageNumber, pageSize);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = ParameterModel.ItemDefaultValue.Timeout
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var get = JsonConvert.DeserializeObject<AllUserRoot>(content);
                    root = get;
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
            return root;
        }
    }
}
