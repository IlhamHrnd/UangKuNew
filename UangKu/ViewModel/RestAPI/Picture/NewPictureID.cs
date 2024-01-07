using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Picture
{
    public class NewPictureID
    {
        private const string GetNewPictureIDEndPoint = "https://uangkuapi.azurewebsites.net/UserPicture/GetNewPictureID?TransType={0}";

        public static async Task<string> GetNewPictureID(string transType)
        {
            string pictureID = string.Empty;
            string url = string.Format(GetNewPictureIDEndPoint, transType);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = Converter.StringToInt(Model.Session.AppParameter.Timeout, ParameterModel.AppParameterDefault.Timeout)
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var get = JsonConvert.DeserializeObject<string>(content);
                    pictureID = get;
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

            return pictureID;
        }
    }
}
