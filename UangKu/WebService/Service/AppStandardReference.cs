using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class AppStandardReference : BaseModel
    {
        public static async Task<Data.Root<List<Data.AppStandardReference.Data>>> GetAllReferenceID(Filter.Root<Filter.AppStandardReference> filter)
        {
            var data = new Data.Root<List<Data.AppStandardReference.Data>>();
            string url = string.Format("{0}AppStandardReference/GetAllReferenceID?PageNumber={1}&PageSize={2}", URL, filter.PageNumber, filter.PageSize);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.AppStandardReference.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.AppStandardReference.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.AppStandardReference.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.AppStandardReference.Data>> GetReferenceID(Filter.Root<Filter.AppStandardReference> filter)
        {
            var data = new Data.Root<Data.AppStandardReference.Data>();
            string url = string.Format("{0}AppStandardReference/GetReferenceID?ReferenceID={1}", URL, filter.Data.ReferenceID);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Get,
                Timeout = TimeSpan.FromSeconds(TimeOut),
            };
            var response = await client.ExecuteGetAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                    data = JsonConvert.DeserializeObject<Data.Root<Data.AppStandardReference.Data>>(response.Content);
                else
                    data = new Data.Root<Data.AppStandardReference.Data>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.AppStandardReference.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.AppStandardReference.Data>> CreateAppStandardReference(Data.AppStandardReference.Data asr)
        {
            var data = new Data.Root<Data.AppStandardReference.Data>();
            string url = string.Format("{0}AppStandardReference/CreateAppStandardReference", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(asr);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.AppStandardReference.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.AppStandardReference.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.AppStandardReference.Data>> UpdateAppStandardReference(Data.AppStandardReference.Data asr)
        {
            var data = new Data.Root<Data.AppStandardReference.Data>();
            string url = string.Format("{0}AppStandardReference/UpdateAppStandardReference", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(asr);
            var response = await client.ExecutePatchAsync(request);

            try
            {
                data = new Data.Root<Data.AppStandardReference.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.AppStandardReference.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}