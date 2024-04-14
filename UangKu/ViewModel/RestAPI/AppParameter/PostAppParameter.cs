using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.AppParameter
{
    public class PostAppParameter : BaseModel
    {
        private const string PostAppParameterEndPoint = "{0}AppParameter/PostAppParameter";

        public static async Task<string> PostAppParameterID(Model.Index.Body.PostAppParameter parameter)
        {
            string url = string.Format(PostAppParameterEndPoint, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeOut
            };
            var body = new Model.Index.Body.PostAppParameter
            {
                parameterID = parameter.parameterID,
                parameterName = parameter.parameterName,
                parameterValue = parameter.parameterValue,
                lastUpdateDateTime = parameter.lastUpdateDateTime,
                lastUpdateByUserID = parameter.lastUpdateByUserID,
                isUsedBySystem = parameter.isUsedBySystem,
                srControl = parameter.srControl
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
