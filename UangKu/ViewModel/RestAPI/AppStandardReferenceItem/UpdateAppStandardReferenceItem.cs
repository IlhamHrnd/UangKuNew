using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.AppStandardReferenceItem
{
    public class UpdateAppStandardReferenceItem : BaseModel
    {
        private const string UpdateASRIEndPoint = "{7}AppStandardReferenceItem/UpdateAppStandardReferenceItem?referenceID={0}&itemID={1}&itemName={2}&note={3}&isActive={4}&isUse={5}&user={6}";

        public static async Task<string> PatchASRI(string referenceID, string itemID, string itemName, string note, bool isActive, bool isUse, string user)
        {
            string result;
            string url = string.Format(UpdateASRIEndPoint, referenceID, itemID, itemName, note, isActive, isUse, user, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeOut
            };
            var response = await client.PatchAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.Substring(1, response.Content.Length - 2);
                }
                else
                {
                    result = response.Content;
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}
