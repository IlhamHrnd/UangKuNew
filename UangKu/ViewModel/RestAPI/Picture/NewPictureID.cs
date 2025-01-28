using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.GenerateAutoNumber.AutoNumber;

namespace UangKu.ViewModel.RestAPI.Picture
{
    public class NewPictureID : BaseModel
    {
        private const string GetNewPictureIDEndPoint = "{1}UserPicture/GetNewPictureID?TransType={0}";

        public static async Task<AutoNumberRoot> GetNewPictureID(string transType)
        {
            AutoNumberRoot root = new AutoNumberRoot();
            string url = string.Format(GetNewPictureIDEndPoint, transType, URL);
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
                    var content = JsonConvert.DeserializeObject<string>(response.Content);
                    root = new AutoNumberRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Transaction {response.StatusDescription}"
                        },
                        AutoNumber = content
                    };
                }
                else
                {
                    root = new AutoNumberRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Transaction {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new AutoNumberRoot
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
