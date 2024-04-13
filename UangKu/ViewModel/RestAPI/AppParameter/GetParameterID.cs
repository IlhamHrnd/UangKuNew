using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppParameter.ParameterID;

namespace UangKu.ViewModel.RestAPI.AppParameter
{
    public class GetParameterID : BaseModel
    {
        private const string ParameterIDEndPoint = "{1}AppParameter/GetParameterID?parameterID={0}";

        public static async Task<ParameterIDRoot> GetParameter(string parameterID)
        {
            ParameterIDRoot root = new ParameterIDRoot();
            string url = string.Format(ParameterIDEndPoint, parameterID, SessionModel.APIUrlLink());
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = TimeOut
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var format = content.Substring(1, content.Length - 2);
                    var get = JsonConvert.DeserializeObject<ParameterIDRoot>(format);
                    root = get;
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
            return root;
        }
    }
}
