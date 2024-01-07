using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Profile
{
    public class PatchProfile
    {
        private const string ProfileEndPoint = "https://uangkuapi.azurewebsites.net/Profile/PatchProfile";

        public static async Task<string> PatchProfileID(Model.Index.Body.PatchProfile profile)
        {
            string url = string.Format(ProfileEndPoint);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = Converter.StringToInt(Model.Session.AppParameter.Timeout, ParameterModel.AppParameterDefault.Timeout)
            };
            var body = new Model.Index.Body.PatchProfile
            {
                personID = profile.personID,
                firstName = profile.firstName,
                middleName = profile.middleName,
                lastName = profile.lastName,
                birthDate = profile.birthDate,
                placeOfBirth = profile.placeOfBirth,
                address = profile.address,
                photo = profile.photo,
                province = profile.province,
                city = profile.city,
                district = profile.district,
                subdistrict = profile.subdistrict,
                postalCode = profile.postalCode,
                lastUpdateDateTime = profile.lastUpdateDateTime,
                lastUpdateByUser = profile.lastUpdateByUser
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
