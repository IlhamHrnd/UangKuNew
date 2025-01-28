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
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var format = response.Content.Substring(1, response.Content.Length - 2);
                    var content = JsonConvert.DeserializeObject<Datum>(format);
                    root = new AppStandardReferenceIDRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"App Standard Reference {response.StatusDescription}"
                        },
                        data = content
                    };
                }
                else
                {
                    root = new AppStandardReferenceIDRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"App Standard Reference {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new AppStandardReferenceIDRoot
                {
                    metaData = new MetaData
                    {
                        code = 201,
                        isSucces = false,
                        message = ex.Message
                    }
                };
            }
            return root;
        }
    }
}
