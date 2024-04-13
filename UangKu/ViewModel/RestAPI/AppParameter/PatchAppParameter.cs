using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.AppParameter
{
    public class PatchAppParameter : BaseModel
    {
        private const string PatchParameterEndPoint = "{0}AppParameter/UpdateAppParameter";

        public static async Task<string> PatchParameterID(Model.Index.Body.PatchParameter parameter)
        {
            string url = string.Format(PatchParameterEndPoint, SessionModel.APIUrlLink());
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeOut
            };
            var body = new Model.Index.Body.PatchParameter
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

            var response = await client.ExecuteAsync(request);

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
