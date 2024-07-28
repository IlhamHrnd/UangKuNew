using RestSharp;
using UangKu.Model.Base;
using UangKu.Model.Index.Body;

namespace UangKu.ViewModel.RestAPI.AppStandardReferenceItem
{
    public class PostAppStandardReferenceItem : BaseModel
    {
        private const string PostASRIEndPoint = "{0}AppStandardReferenceItem/CreateAppStandardReferenceItem";

        public static async Task<string> PostASRI(string referenceID, string itemID, string itemName, string note, string itemIcon)
        {
            string result;
            string url = string.Format(PostASRIEndPoint, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeOut
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
