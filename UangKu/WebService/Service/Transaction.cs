using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class Transaction : BaseModel
    {
        public async Task<Data.Root<Data.Transaction.Data>> PostTransaction(Data.Transaction.Data trans)
        {
            var data = new Data.Root<Data.Transaction.Data>();
            string url = string.Format("{0}Transaction/PostTransaction", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(trans);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.Transaction.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.Transaction.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public async Task<Data.Root<Data.Transaction.Data>> PatchTransaction(Data.Transaction.Data trans)
        {
            var data = new Data.Root<Data.Transaction.Data>();
            string url = string.Format("{0}Transaction/PatchTransaction", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(trans);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.Transaction.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.Transaction.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public async Task<Data.Root<List<Data.Transaction.Data>>> GetAllTransaction(Filter.Root<Filter.Transaction> filter)
        {
            var data = new Data.Root<List<Data.Transaction.Data>>();
            string url = string.Format("{0}Transaction/GetAllTransaction?PersonID={1}&StartDate={2}&EndDate={3}&OrderBy={4}&IsAscending={5}&PageNumber={6}&PageSize={7}", 
                URL, filter.Data.PersonID, filter.Data.StartDate, filter.Data.EndDate, filter.Data.OrderBy, filter.Data.IsAscending, filter.PageNumber, filter.PageSize);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.Transaction.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.Transaction.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.Transaction.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public async Task<Data.Root<List<Data.Transaction.Data>>> GetAllPDFTransaction(Filter.Root<Filter.Transaction> filter)
        {
            var data = new Data.Root<List<Data.Transaction.Data>>();
            string url = string.Format("{0}Transaction/GetAllPDFTransaction?PersonID={1}&StartDate={2}&EndDate={3}&OrderBy={4}&IsAscending={5}",
                URL, filter.Data.PersonID, filter.Data.StartDate, filter.Data.EndDate, filter.Data.OrderBy, filter.Data.IsAscending);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.Transaction.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.Transaction.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.Transaction.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public async Task<Data.Root<Data.Transaction.Data>> GetTransactionNo(Filter.Root<Filter.Transaction> filter)
        {
            var data = new Data.Root<Data.Transaction.Data>();
            string url = string.Format("{0}Transaction/GetTransactionNo?TransNo={1}", URL, filter.Data.TransNo);
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
                    data = JsonConvert.DeserializeObject<Data.Root<Data.Transaction.Data>>(response.Content);
                else
                    data = new Data.Root<Data.Transaction.Data>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.Transaction.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public async Task<Data.Root<Data.Transaction.Data>> GetSumTransaction(Filter.Root<Filter.Transaction> filter)
        {
            var data = new Data.Root<Data.Transaction.Data>();
            string url = string.Format("{0}Transaction/GetSumTransaction?PersonID={1}&StartDate={2}&EndDate={3}", URL, filter.Data.PersonID, filter.Data.StartDate, filter.Data.EndDate);
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
                    data = JsonConvert.DeserializeObject<Data.Root<Data.Transaction.Data>>(response.Content);
                else
                    data = new Data.Root<Data.Transaction.Data>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.Transaction.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public async Task<Data.Root<string>> GenerateReportTransaction(Filter.Root<Filter.Transaction> filter)
        {
            var data = new Data.Root<string>();
            string url = string.Format("{0}Transaction/GenerateReportTransaction?PersonID={1}&StartDate={2}&EndDate={3}&OrderBy={4}&IsAscending={5}", URL, filter.Data.PersonID,
                filter.Data.StartDate, filter.Data.EndDate, filter.Data.OrderBy, filter.Data.IsAscending);
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
                    data = JsonConvert.DeserializeObject<Data.Root<string>>(response.Content);
                else
                    data = new Data.Root<string>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<string>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}
