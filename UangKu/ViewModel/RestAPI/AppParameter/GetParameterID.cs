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
            string url = string.Format(ParameterIDEndPoint, parameterID, URL);
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
                    root = new ParameterIDRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Parameter {response.StatusDescription}"
                        },
                        data = content
                    };
                }
                else
                {
                    root = new ParameterIDRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Parameter {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new ParameterIDRoot
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
