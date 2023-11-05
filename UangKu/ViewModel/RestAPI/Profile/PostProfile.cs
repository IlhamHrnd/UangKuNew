﻿using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Profile
{
    public class PostProfile
    {
        private const string ProfileEndPoint = "https://uangkuapi.azurewebsites.net/Profile/PostProfile";

        public static async Task<string> PostProfileID(Model.Index.Body.PostProfile profile)
        {
            string url = string.Format(ProfileEndPoint);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = ParameterModel.ItemDefaultValue.Timeout
            };
            var body = new Model.Index.Body.PostProfile
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
