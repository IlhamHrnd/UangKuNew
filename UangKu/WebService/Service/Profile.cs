using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class Profile : BaseModel
    {
        public static async Task<Data.Root<Data.Profile.Data>> GetPersonID(Filter.Root<Filter.Profile> filter)
        {
            var data = new Data.Root<Data.Profile.Data>();
            string url = string.Format("{0}Profile/GetPersonID?PersonID={1}", URL, filter.Data.PersonID);
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
                    data = JsonConvert.DeserializeObject<Data.Root<Data.Profile.Data>>(response.Content);
                else
                    data = new Data.Root<Data.Profile.Data>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.Profile.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.Profile.Data>> PostProfile(Data.Profile.Data profile)
        {
            var data = new Data.Root<Data.Profile.Data>();
            string url = string.Format("{0}Profile/PostProfile", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(profile);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.Profile.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.Profile.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.Profile.Data>> PatchProfile(Data.Profile.Data profile)
        {
            var data = new Data.Root<Data.Profile.Data>();
            string url = string.Format("{0}Profile/PatchProfile", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(profile);
            var response = await client.ExecutePatchAsync(request);

            try
            {
                data = new Data.Root<Data.Profile.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.Profile.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}
