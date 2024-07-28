using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Index.Body;

namespace UangKu.ViewModel.RestAPI.AppStandardReferenceItem
{
    public class PostAppStandardReference : BaseModel
    {
        private const string PostASREndPoint = "{0}AppStandardReference/CreateAppStandardReference";

        public static async Task<string> PostASR(string referenceID, string referenceName, int itemLength, string note)
        {
            string result;
            string url = string.Format(PostASREndPoint, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeOut
            };
            var body = new PostASR
            {
                standardReferenceID = referenceID,
                standardReferenceName = referenceName,
                itemLength = itemLength,
                isUsedBySystem = ParameterModel.ItemDefaultValue.IsUsed,
                isActive = ParameterModel.ItemDefaultValue.IsActive,
                note = note,
                lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                lastUpdateByUserID = App.Session.username
            };
            request.AddJsonBody(body);

            var response = await client.ExecutePostAsync(request);

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
