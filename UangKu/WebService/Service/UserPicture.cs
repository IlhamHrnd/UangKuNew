using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class UserPicture : BaseModel
    {
        public static async Task<Data.Root<List<Data.UserPicture.Data>>> GetUserPicture(Filter.Root<Filter.UserPicture> filter)
        {
            var data = new Data.Root<List<Data.UserPicture.Data>>();
            string url = string.Format("{0}UserPicture/GetUserPicture?PersonID={1}&IsDelete={2}&PageNumber={3}&PageSize={4}", URL, filter.Data.PersonID, filter.Data.IsDelete, filter.PageNumber, filter.PageSize);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.UserPicture.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.UserPicture.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.UserPicture.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.UserPicture.Data>> PostUserPicture(Data.UserPicture.Data pic)
        {
            var data = new Data.Root<Data.UserPicture.Data>();
            string url = string.Format("{0}UserPicture/PostUserPicture", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(pic);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.UserPicture.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.UserPicture.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.UserPicture.Data>> DeleteUserPicture(Data.UserPicture.Data pic)
        {
            var data = new Data.Root<Data.UserPicture.Data>();
            string url = string.Format("{0}UserPicture/DeleteUserPicture", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Delete,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(pic);
            var response = await client.ExecuteDeleteAsync(request);

            try
            {
                data = new Data.Root<Data.UserPicture.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.UserPicture.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}
