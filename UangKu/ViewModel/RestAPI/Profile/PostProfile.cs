using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Profile
{
    public class PostProfile
    {
        private const string PostProfileEndPoint = "https://uangkuapi.azurewebsites.net/Profile/PostProfile";

        public static async Task<string> PostProfileID(Model.Index.Body.PostProfile root)
        {
            string url = string.Format(PostProfileEndPoint);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = ParameterModel.ItemDefaultValue.Timeout
            };
            var body = new Model.Index.Body.PostProfile
            {
                personID = root.personID,
                firstName = root.firstName,
                middleName = root.middleName,
                lastName = root.lastName,
                birthDate = root.birthDate,
                placeOfBirth = root.placeOfBirth,
                photo = root.photo,
                address = root.address,
                province = root.province,
                city = root.city,
                district = root.district,
                subdistrict = root.subdistrict,
                postalCode = root.postalCode,
                lastUpdateDateTime = root.lastUpdateDateTime,
                lastUpdateByUser = root.lastUpdateByUser
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
