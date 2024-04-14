﻿using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;
using static UangKu.Model.Response.Location.PostalCode;

namespace UangKu.ViewModel.RestAPI.Location
{
    public class GetPostalCode : BaseModel
    {
        private const string PostalCodeEndPoint = "{4}Location/GetPostalCode?SubdisID={0}&DisID={1}&CityID={2}&ProvID={3}";

        public static async Task<PostalCodeRoot> GetPostalCodes(string provID, string cityID, string disID, string subdisID)
        {
            PostalCodeRoot root = new PostalCodeRoot();
            string url = string.Format(PostalCodeEndPoint, subdisID, disID, cityID, provID, URL);
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
                    var get = JsonConvert.DeserializeObject<PostalCodeRoot>(format);
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
