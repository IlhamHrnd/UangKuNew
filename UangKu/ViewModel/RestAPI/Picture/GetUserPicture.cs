using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Picture.UserPicture;

namespace UangKu.ViewModel.RestAPI.Picture
{
    public class GetUserPicture
    {
        private const string GetUserPictureEndPoint = "https://uangkuapi.azurewebsites.net/UserPicture/GetUserPicture?PageNumber={0}&PageSize={1}&PersonID={2}&IsDeleted={3}";

        public static async Task<UserPictureRoot> GetAllUserPicture(int pageNumber, int pageSize, string personID, bool isDeleted)
        {
            UserPictureRoot root = new UserPictureRoot();
            string url = string.Format(GetUserPictureEndPoint, pageNumber, pageSize, personID, isDeleted);
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
                    var get = JsonConvert.DeserializeObject<UserPictureRoot>(content);
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
