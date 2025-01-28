using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class User : BaseModel
    {
        public static async Task<Data.Root<Data.User.Data>> GetLoginUserName(Filter.Root<Filter.User> filter)
        {
            var data = new Data.Root<Data.User.Data>();
            string url = string.Format("{0}User/GetLoginUserName?Username={1}&Password={2}", URL, filter.Data.Username, filter.Data.Password);
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
                    data = JsonConvert.DeserializeObject<Data.Root<Data.User.Data>>(response.Content);
                else
                    data = new Data.Root<Data.User.Data>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.User.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.User.Data>> UpdateLastLogin(Filter.Root<Filter.User> filter)
        {
            var data = new Data.Root<Data.User.Data>();
            string url = string.Format("{0}User/UpdateLastLogin?Username={1}", URL, filter.Data.Username);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,    
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            var response = await client.ExecutePatchAsync(request);

            try
            {
                data = new Data.Root<Data.User.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.User.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.User.Data>> GetUsername(Filter.Root<Filter.User> filter)
        {
            var data = new Data.Root<Data.User.Data>();
            string url = string.Format("{0}User/GetUsername?Username={1}", URL, filter.Data.Username);
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
                    data = JsonConvert.DeserializeObject<Data.Root<Data.User.Data>>(response.Content);
                else
                    data = new Data.Root<Data.User.Data>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.User.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<List<Data.User.Data>>> GetAllUser(Filter.Root<Filter.User> filter)
        {
            var data = new Data.Root<List<Data.User.Data>>();
            string url = string.Format("{0}User/GetAllUser?PageNumber={1}&PageSize={2}", URL, filter.PageNumber, filter.PageSize);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.User.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.User.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.User.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.User.Data>> CreateUsername(Data.User.Data user)
        {
            var data = new Data.Root<Data.User.Data>();
            string url = string.Format("{0}User/CreateUsername", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(user);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.User.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.User.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.User.Data>> UpdateUsername(Data.User.Data user)
        {
            var data = new Data.Root<Data.User.Data>();
            string url = string.Format("{0}User/UpdateUsername", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(user);
            var response = await client.ExecutePatchAsync(request);

            try
            {
                data = new Data.Root<Data.User.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.User.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.User.Data>> UpdatePasswordUser(Filter.Root<Filter.User> filter)
        {
            var data = new Data.Root<Data.User.Data>();
            string url = string.Format("{0}User/UpdatePasswordUser?Username={1}&Password={2}&Email={3}", URL, filter.Data.Username, filter.Data.Password, filter.Data.Email);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            var response = await client.ExecutePatchAsync(request);

            try
            {
                data = new Data.Root<Data.User.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.User.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}
