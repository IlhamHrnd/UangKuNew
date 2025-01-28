using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class UserReport : BaseModel
    {
        public static async Task<Data.Root<Data.UserReport.Data>> PostUserReport(Data.UserReport.Data rpt)
        {
            var data = new Data.Root<Data.UserReport.Data>();
            string url = string.Format("{0}UserReport/PostUserReport", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(rpt);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.UserReport.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.UserReport.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.UserReport.Data>> PatchUserReport(Data.UserReport.Data rpt)
        {
            var data = new Data.Root<Data.UserReport.Data>();
            string url = string.Format("{0}UserReport/PatchUserReport", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(rpt);
            var response = await client.ExecutePatchAsync(request);

            try
            {
                data = new Data.Root<Data.UserReport.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.UserReport.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<List<Data.UserReport.Data>>> GetUserReport(Filter.Root<Filter.UserReport> filter)
        {
            var data = new Data.Root<List<Data.UserReport.Data>>();
            string url = string.Format("{0}UserReport/GetUserReport?IsApproved={1}&PersonID={2}", URL, filter.Data.IsApproved, filter.Data.PersonID);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.UserReport.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.UserReport.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.UserReport.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<List<Data.UserReport.Data>>> GetReportNo(Filter.Root<Filter.UserReport> filter)
        {
            var data = new Data.Root<List<Data.UserReport.Data>>();
            string url = string.Format("{0}UserReport/GetReportNo?ReportNo={1}", URL, filter.Data.ReportNo);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.UserReport.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.UserReport.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.UserReport.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}
