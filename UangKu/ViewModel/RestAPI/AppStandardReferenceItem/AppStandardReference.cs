using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReference.AppStandardReference;

namespace UangKu.ViewModel.RestAPI.AppStandardReferenceItem
{
    public class AppStandardReference : BaseModel
    {
        private const string AppStandardReferenceEndPoint = "{2}AppStandardReference/GetAllReferenceID?PageNumber={0}&PageSize={1}";

        public static async Task<AppStandardReferenceRoot> GetAllASR(int pageNumber, int pageSize)
        {
            AppStandardReferenceRoot root = new AppStandardReferenceRoot();
            string url = string.Format(AppStandardReferenceEndPoint, pageNumber, pageSize, URL);
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
                    var get = JsonConvert.DeserializeObject<AppStandardReferenceRoot>(content);
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
