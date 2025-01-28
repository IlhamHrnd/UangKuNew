using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class AppParameter : BaseModel
    {
        public static async Task<Data.Root<List<Data.AppParameter.Data>>> GetAllAppParameter(Filter.Root<Filter.AppParameter> filter)
        {
            var data = new Data.Root<List<Data.AppParameter.Data>>();
            string url = string.Format("{0}AppParameter/GetAllAppParameter?PageNumber={1}&PageSize={2}", URL, filter.PageNumber, filter.PageSize);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.AppParameter.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.AppParameter.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.AppParameter.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<List<Data.AppParameter.Data>>> GetAllParameterWithNoPageFilter()
        {
            var data = new Data.Root<List<Data.AppParameter.Data>>();
            string url = string.Format("{0}AppParameter/GetAllParameterWithNoPageFilter", URL);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.AppParameter.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.AppParameter.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.AppParameter.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.AppParameter.Data>> GetParameterID(Filter.Root<Filter.AppParameter> filter)
        {
            var data = new Data.Root<Data.AppParameter.Data>();
            string url = string.Format("{0}AppParameter/GetParameterID?ParameterID={1}", URL, filter.Data.ParameterID);
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
                    data = JsonConvert.DeserializeObject<Data.Root<Data.AppParameter.Data>>(response.Content);
                else
                    data = new Data.Root<Data.AppParameter.Data>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.AppParameter.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }

            return data;
        }

        public static async Task<Data.Root<Data.AppParameter.Data>> PostAppParameter(Data.AppParameter.Data param)
        {
            var data = new Data.Root<Data.AppParameter.Data>();
            string url = string.Format("{0}AppParameter/PostAppParameter", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(param);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.AppParameter.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.AppParameter.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.AppParameter.Data>> UpdateAppParameter(Data.AppParameter.Data param)
        {
            var data = new Data.Root<Data.AppParameter.Data>();
            string url = string.Format("{0}AppParameter/UpdateAppParameter", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(param);
            var response = await client.ExecutePatchAsync(request);

            try
            {
                data = new Data.Root<Data.AppParameter.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.AppParameter.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}
