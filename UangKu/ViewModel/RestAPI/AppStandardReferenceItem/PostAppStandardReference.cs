using Newtonsoft.Json;
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
            string url = string.Format(PostASREndPoint, SessionModel.APIUrlLink());
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
                    var content = response.Content;
                    var post = JsonConvert.DeserializeObject<string>(content);
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
