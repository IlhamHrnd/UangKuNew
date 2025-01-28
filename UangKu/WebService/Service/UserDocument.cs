using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class UserDocument : BaseModel
    {
        public static async Task<Data.Root<Data.UserDocument.Data>> PostUserDocument(Data.UserDocument.Data doc)
        {
            var data = new Data.Root<Data.UserDocument.Data>();
            string url = string.Format("{0}UserDocument/PostUserDocument", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(doc);
            var response = await client.ExecutePostAsync(request);

            try
            {
                data = new Data.Root<Data.UserDocument.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.UserDocument.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.UserDocument.Data>> PatchUserDocument(Data.UserDocument.Data doc)
        {
            var data = new Data.Root<Data.UserDocument.Data>();
            string url = string.Format("{0}UserDocument/PatchUserDocument", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Patch,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(doc);
            var response = await client.ExecutePatchAsync(request);

            try
            {
                data = new Data.Root<Data.UserDocument.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.UserDocument.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<List<Data.UserDocument.Data>>> GetUserDocument(Filter.Root<Filter.UserDocument> filter)
        {
            var data = new Data.Root<List<Data.UserDocument.Data>>();
            string url = string.Format("{0}UserDocument/GetUserDocument?PersonID={1}&IsDeleted={2}&PageNumber={3}&PageSize={4}", URL, filter.Data.PersonID, filter.Data.IsDeleted,
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.UserDocument.Data>>>(response.Content);
                else
                    data = new Data.Root<List<Data.UserDocument.Data>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.UserDocument.Data>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.UserDocument.Data>> GetUserDocumentID(Filter.Root<Filter.UserDocument> filter)
        {
            var data = new Data.Root<Data.UserDocument.Data>();
            string url = string.Format("{0}UserDocument/GetUserDocumentID?DocumentID={1}&PersonID={2}", URL, filter.Data.DocumentID, filter.Data.PersonID);
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
                    data = JsonConvert.DeserializeObject<Data.Root<Data.UserDocument.Data>>(response.Content);
                else
                    data = new Data.Root<Data.UserDocument.Data>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.UserDocument.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.UserDocument.Data>> DeleteUserDocument(Data.UserDocument.Data doc)
        {
            var data = new Data.Root<Data.UserDocument.Data>();
            string url = string.Format("{0}UserDocument/DeleteUserDocument", URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Delete,
                Timeout = TimeSpan.FromSeconds(TimeOut)
            };
            request.AddJsonBody(doc);
            var response = await client.ExecuteDeleteAsync(request);

            try
            {
                data = new Data.Root<Data.UserDocument.Data>
                {
                    Succeeded = response.IsSuccessStatusCode,
                    Message = response.Content[1..^1]
                };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.UserDocument.Data>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}
