using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class AppStandardReferenceItem : BaseModel
    {
        public static async Task<Data.Root<List<Data.AppStandardReferenceItem.Data>>> GetAllReferenceItemID(Filter.Root<Filter.AppStandardReferenceItem> filter)
        {
            var data = new Data.Root<List<Data.AppStandardReferenceItem.Data>>();
            string url = string.Format("{0}AppStandardReferenceItem/GetAllReferenceItemID?StandardReferenceID={1}&IsActive={2}&IsUsedBySystem={3}", URL, filter.Data.StandardReferenceID, 
                filter.Data.IsActive, filter.Data.IsUsedBySystem);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.AppStandardReferenceItem.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.AppStandardReferenceItem.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.AppStandardReferenceItem.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.AppStandardReferenceItem.Data>> CreateAppStandardReferenceItem(Data.AppStandardReferenceItem.Data asri)
        {
            var data = new Data.Root<Data.AppStandardReferenceItem.Data>();
            string url = string.Format("{0}AppStandardReferenceItem/CreateAppStandardReferenceItem");
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(asri);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.AppStandardReferenceItem.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.AppStandardReferenceItem.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.AppStandardReferenceItem.Data>> UpdateAppStandardReferenceItem(Data.AppStandardReferenceItem.Data asri)
        {
            var data = new Data.Root<Data.AppStandardReferenceItem.Data>();
            string url = string.Format("{0}AppStandardReferenceItem/UpdateAppStandardReferenceItem");
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(asri);
            var response = await client.ExecutePatchAsync(request);

            try
            {
                data = new Data.Root<Data.AppStandardReferenceItem.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.AppStandardReferenceItem.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}
