using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.AppStandardReferenceItem
{
    public class UpdateAppStandardReference : BaseModel
    {
        private const string UpdateASREndPoint = "{6}AppStandardReference/UpdateAppStandardReference?referenceID={0}&itemLength={1}&isActive={2}&isUse={3}&user={4}&note={5}";

        public static async Task<string> PatchASR(string referenceid, int length, bool isactive, bool isuse, string user, string note)
        {
            string result;
            string url = string.Format(UpdateASREndPoint, referenceid, length, isactive, isuse, user, note, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Timeout = TimeOut,
                Method = Method.Patch
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
                    result = response.ErrorMessage;
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
