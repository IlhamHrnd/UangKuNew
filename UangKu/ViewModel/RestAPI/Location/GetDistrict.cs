using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Location.District;

namespace UangKu.ViewModel.RestAPI.Location
{
    public class GetDistrict : BaseModel
    {
        private const string GetDistrictEndPoint = "{1}Location/GetAllDistrict?CityID={0}";

        public static async Task<List<DistrictRoot>> GetDistricts(string cityID)
        {
            List<DistrictRoot> root = new List<DistrictRoot>();
            string url = string.Format(GetDistrictEndPoint, cityID, SessionModel.APIUrlLink());
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
                    var get = JsonConvert.DeserializeObject<List<DistrictRoot>>(content);
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
