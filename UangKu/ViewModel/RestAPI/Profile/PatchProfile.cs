using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Profile
{
    public class PatchProfile : BaseModel
    {
        private const string ProfileEndPoint = "{0}Profile/PatchProfile";

        public static async Task<string> PatchProfileID(Model.Index.Body.PatchProfile profile)
        {
            string result;
            string url = string.Format(ProfileEndPoint, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeOut
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
                    result = response.Content.Substring(1, response.Content.Length - 2);
                }
                else
                {
                    result = response.Content;
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}
