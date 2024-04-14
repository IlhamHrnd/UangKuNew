using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReference.AppStandardReferenceID;

namespace UangKu.ViewModel.RestAPI.AppStandardReferenceItem
{
    public class GetAppStandardReferenceID : BaseModel
    {
        private const string AppStandardReferenceIDEndPoint = "{1}AppStandardReference/GetReferenceID?ReferenceID={0}";

        public static async Task<AppStandardReferenceIDRoot> GetASRAsync(string standardid)
        {
            AppStandardReferenceIDRoot root = new AppStandardReferenceIDRoot();
            string url = string.Format(AppStandardReferenceIDEndPoint, standardid, URL);
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
                    var get = JsonConvert.DeserializeObject<AppStandardReferenceIDRoot>(format);
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
