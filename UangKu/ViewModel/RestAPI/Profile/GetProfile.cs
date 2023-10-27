using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Profile.GetProfile;

namespace UangKu.ViewModel.RestAPI.Profile
{
    public class GetProfile
    {
        private const string ProfileEndPoint = "https://uangkuapi.azurewebsites.net/Profile/GetPersonID?PersonID={0}";

        public static async Task<GetProfileRoot> GetProfileID(string personID)
        {
            GetProfileRoot root = new GetProfileRoot();
            string url = string.Format(ProfileEndPoint, personID);
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
                    var format = content.Substring(1, content.Length - 2);
                    var get = JsonConvert.DeserializeObject<GetProfileRoot>(format);
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
