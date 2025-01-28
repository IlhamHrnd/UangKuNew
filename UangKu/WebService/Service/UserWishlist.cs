using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class UserWishlist : BaseModel
    {
        public static async Task<Data.Root<List<Data.UserWishlist.Data>>> GetAllUserWishlist(Filter.Root<Filter.UserWishlist> filter)
        {
            var data = new Data.Root<List<Data.UserWishlist.Data>>();
            string url = string.Format("{0}UserWishlist/GetAllUserWishlist?PersonID={1}&PageNumber={2}&PageSize={3}", URL, filter.Data.PersonID,
                filter.PageNumber, filter.PageSize);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.UserWishlist.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.UserWishlist.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.UserWishlist.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.UserWishlist.Data>> GetUserWishlistID(Filter.Root<Filter.UserWishlist> filter)
        {
            var data = new Data.Root<Data.UserWishlist.Data>();
            string url = string.Format("{0}UserWishlist/GetUserWishlistID?WishlistID={1}", URL, filter.Data.WishlistID);
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
                    data = JsonConvert.DeserializeObject<Data.Root<Data.UserWishlist.Data>>(response.Content);
                else
                    data = new Data.Root<Data.UserWishlist.Data>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.UserWishlist.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.UserWishlist.Data>> PostUserWishlist(Data.UserWishlist.Data wish)
        {
            var data = new Data.Root<Data.UserWishlist.Data>();
            string url = string.Format("{0}UserWishlist/PostUserWishlist", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(wish);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.UserWishlist.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.UserWishlist.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.UserWishlist.Data>> PatchUserWishlist(Data.UserWishlist.Data wish)
        {
            var data = new Data.Root<Data.UserWishlist.Data>();
            string url = string.Format("{0}UserWishlist/PatchUserWishlist", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(wish);
            var response = await client.ExecutePatchAsync(request);

            try
            {
                data = new Data.Root<Data.UserWishlist.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.UserWishlist.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<List<Data.UserWishlist.Data>>> GetUserWishlistPerCategory(Filter.Root<Filter.UserWishlist> filter)
        {
            var data = new Data.Root<List<Data.UserWishlist.Data>>();
            string url = string.Format("{0}UserWishlist/GetUserWishlistPerCategory?PersonID={1}&IsComplete={2}", URL, filter.Data.PersonID, filter.Data.IsComplete);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.UserWishlist.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.UserWishlist.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.UserWishlist.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}
