using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class Location : BaseModel
    {
        public static async Task<Data.Root<List<Data.Location.Province>>> GetAllProvince()
        {
            var data = new Data.Root<List<Data.Location.Province>>();
            string url = string.Format("{0}Location/GetAllProvince", URL);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.Location.Province>>>(response.Content);
                else
                    data = new Data.Root<List<Data.Location.Province>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.Location.Province>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<List<Data.Location.City>>> GetAllCities(Filter.Root<Filter.Location> filter)
        {
            var data = new Data.Root<List<Data.Location.City>>();
            string url = string.Format("{0}Location/GetAllCities?ProvID={1}", URL, filter.Data.ProvID);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.Location.City>>>(response.Content);
                else
                    data = new Data.Root<List<Data.Location.City>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.Location.City>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<List<Data.Location.District>>> GetAllDistrict(Filter.Root<Filter.Location> filter)
        {
            var data = new Data.Root<List<Data.Location.District>>();
            string url = string.Format("{0}Location/GetAllDistrict?CityID={1}", URL, filter.Data.CityID);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.Location.District>>>(response.Content);
                else
                    data = new Data.Root<List<Data.Location.District>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.Location.District>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<List<Data.Location.SubDistrict>>> GetAllSubDistrict(Filter.Root<Filter.Location> filter)
        {
            var data = new Data.Root<List<Data.Location.SubDistrict>>();
            string url = string.Format("{0}Location/GetAllSubDistrict?DistrictID={1}", URL, filter.Data.DistrictID);
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
                    data = JsonConvert.DeserializeObject<Data.Root<List<Data.Location.SubDistrict>>>(response.Content);
                else
                    data = new Data.Root<List<Data.Location.SubDistrict>>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<List<Data.Location.SubDistrict>>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }

        public static async Task<Data.Root<Data.Location.PostalCode>> GetPostalCode(Filter.Root<Filter.Location> filter)
        {
            var data = new Data.Root<Data.Location.PostalCode>();
            string url = string.Format("{0}Location/GetPostalCode?ProvID={1}&CityID={2}&DistrictID={3}&SubDisID={4}", URL, filter.Data.ProvID, filter.Data.CityID, 
                filter.Data.DistrictID, filter.Data.SubDisID);
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
                    data = JsonConvert.DeserializeObject<Data.Root<Data.Location.PostalCode>>(response.Content);
                else
                    data = new Data.Root<Data.Location.PostalCode>
                    {
                        Succeeded = false,
                        Message = !string.IsNullOrEmpty(response.ErrorException.Message) ? response.ErrorException.Message : response.StatusDescription
                    };
            }
            catch (Exception e)
            {
                data = new Data.Root<Data.Location.PostalCode>
                {
                    Succeeded = false,
                    Message = e.Message
                };
            }
            return data;
        }
    }
}
