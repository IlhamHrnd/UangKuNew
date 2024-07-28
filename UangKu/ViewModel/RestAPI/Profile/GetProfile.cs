using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Profile.Profile;

namespace UangKu.ViewModel.RestAPI.Profile
{
    public class GetProfile : BaseModel
    {
        private const string ProfileEndPoint = "{1}Profile/GetPersonID?PersonID={0}";

        public static async Task<ProfileRoot> GetProfileID(string personID)
        {
            ProfileRoot root = new ProfileRoot();
            string url = string.Format(ProfileEndPoint, personID, URL);
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
                    var format = response.Content.Substring(1, response.Content.Length - 2);
                    var content = JsonConvert.DeserializeObject<ProfileRoot>(format);
                    root = new ProfileRoot
                    {
                        metaData = new MetaData
                        {
                            code = 200,
                            isSucces = true,
                            message = $"Transaction {response.StatusDescription}"
                        },
                        personID = content.personID,
                        firstName = content.firstName,
                        middleName = content.middleName,
                        lastName = content.lastName,
                        birthDate = content.birthDate,
                        placeOfBirth = content.placeOfBirth,
                        photo = content.photo,
                        address = content.address,
                        province = content.province,
                        city = content.city,
                        subdistrict = content.subdistrict,
                        district = content.district,
                        postalCode = content.postalCode,
                        lastUpdateDateTime = content.lastUpdateDateTime,
                        lastUpdateByUser = content.lastUpdateByUser,
                        fullName = content.fullName,
                        birthDateFormat = content.birthDateFormat
                    };
                }
                else
                {
                    root = new ProfileRoot
                    {
                        metaData = new MetaData
                        {
                            code = 201,
                            isSucces = false,
                            message = $"Profile {response.StatusDescription}"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                root = new ProfileRoot
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
