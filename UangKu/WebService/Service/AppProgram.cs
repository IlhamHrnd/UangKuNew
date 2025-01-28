using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class AppProgram : BaseModel
    {
        public static async Task<Data.Root<List<Data.AppProgram.Data>>> GetAllAppProgram(Filter.Root<Filter.AppProgram> filter)
        {
            var data = new Data.Root<List<Data.AppProgram.Data>>();
            string url = string.Format("{0}AppProgram/GetAllAppProgram?IsVisible={1}&IsUsedBySystem={2}&PageNumber={3}&PageSize={4}", URL, filter.Data.IsVisible.Value, filter.Data.IsUedBySystem.Value, 
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.AppProgram.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.AppProgram.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.AppProgram.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.AppProgram.Data>> GetAppProgramID(Filter.Root<Filter.AppProgram> filter)
        {
            var data = new Data.Root<Data.AppProgram.Data>();
            string url = string.Format("{0}AppProgram/GetAppProgramID?ProgramID={1}", URL, filter.Data.ProgramID);
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
                    data = JsonConvert.DeserializeObject<Data.Root<Data.AppProgram.Data>>(response.Content);
                else
                    data = new Data.Root<Data.AppProgram.Data>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.AppProgram.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.AppProgram.Data>> PostAppProgram(Data.AppProgram.Data prog)
        {
            var data = new Data.Root<Data.AppProgram.Data>();
            string url = string.Format("{0}AppProgram/PostAppProgram", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(prog);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.AppProgram.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.AppProgram.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.AppProgram.Data>> PatchAppProgram(Data.AppProgram.Data prog)
        {
            var data = new Data.Root<Data.AppProgram.Data>();
            string url = string.Format("{0}AppProgram/PatchAppProgram", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(prog);
            var response = await client.ExecutePatchAsync(request);

            try
            {
                data = new Data.Root<Data.AppProgram.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.AppProgram.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}
