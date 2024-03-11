using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.AppStandardReferenceItem
{
    public class UpdateAppStandardReferenceItem
    {
        private const string UpdateASRIEndPoint = "{7}AppStandardReferenceItem/UpdateAppStandardReferenceItem?referenceID={0}&itemID={1}&itemName={2}&note={3}&isActive={4}&isUse={5}&user={6}";

        public static async Task<string> PatchASRI(string referenceID, string itemID, string itemName, string note, bool isActive, bool isUse, string user)
        {
            string url = string.Format(UpdateASRIEndPoint, referenceID, itemID, itemName, note, isActive, isUse, user, SessionModel.APIUrlLink());
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = Converter.StringToInt(Model.Session.AppParameter.Timeout, ParameterModel.AppParameterDefault.Timeout)
            };
            var response = await client.PatchAsync(request);

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
