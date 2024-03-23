using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Index.Body;

namespace UangKu.ViewModel.RestAPI.AppStandardReferenceItem
{
    public class PostAppStandardReferenceItem
    {
        private const string PostASRIEndPoint = "{0}AppStandardReferenceItem/CreateAppStandardReferenceItem";

        public static async Task<string> PostASRI(string referenceID, string itemID, string itemName, string note, string itemIcon)
        {
            string url = string.Format(PostASRIEndPoint, SessionModel.APIUrlLink());
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = Converter.StringToInt(Model.Session.AppParameter.Timeout, ParameterModel.AppParameterDefault.Timeout)
            };
            var body = new PostASRI
            {
                standardReferenceID = referenceID,
                itemID = itemID,
                itemName = itemName,
                note = note,
                isUsedBySystem = ParameterModel.ItemDefaultValue.IsUsed,
                isActive = ParameterModel.ItemDefaultValue.IsActive,
                lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                lastUpdateByUserID = App.Session.username,
                itemIcon = itemIcon
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
