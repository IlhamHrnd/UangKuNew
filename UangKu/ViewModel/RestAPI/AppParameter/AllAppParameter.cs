using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppParameter.GetAllAppParameter;

namespace UangKu.ViewModel.RestAPI.AppParameter
{
    public class AllAppParameter : BaseModel
    {
        private const string AllAppParameterEndPoint = "{2}AppParameter/GetAllAppParameter?PageNumber={0}&PageSize={1}";

        public static async Task<GetAllAppParameterRoot> GetAllAppParameter(int pageNumber, int pageSize)
        {
            GetAllAppParameterRoot root = new GetAllAppParameterRoot();
            string url = string.Format(AllAppParameterEndPoint, pageNumber, pageSize, SessionModel.APIUrlLink());
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
                    var get = JsonConvert.DeserializeObject<GetAllAppParameterRoot>(content);
                    root = get;
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
            return root;
        }
    }
}
