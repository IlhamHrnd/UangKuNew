using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.AppStandardReferenceItem
{
    public class UpdateAppStandardReference
    {
        private const string UpdateASREndPoint = "https://uangkuapi.azurewebsites.net/AppStandardReference/UpdateAppStandardReference?referenceID={0}&itemLength={1}&isActive={2}&isUse={3}&user={4}&note={5}";

        public static async Task<string> PatchASR(string referenceid, int length, bool isactive, bool isuse, string user, string note)
        {
            string url = string.Format(UpdateASREndPoint, referenceid, length, isactive, isuse, user, note);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Timeout = ParameterModel.ItemDefaultValue.Timeout,
                Method = Method.Patch
            };
            var response = await client.PatchAsync(request);

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
