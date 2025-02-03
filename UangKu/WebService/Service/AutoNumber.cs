using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.WebService.Service
{
    public class AutoNumber : BaseModel
    {
        public static async Task<Data.Root<string>> GenerateAutoNumber(Filter.Root<Filter.AutoNumber> filter)
        {
            var data = new Data.Root<string>();
            string url;

            if (!string.IsNullOrEmpty(filter.Data.ProgramID))
                url = string.Format("{0}AppProgram/GetNewProgramID?ProgramID={1}", URL, filter.Data.ProgramID);
            else if (!string.IsNullOrEmpty(filter.Data.ReferenceID))
                url = string.Format("{0}AppStandardReference/GetNewStandardReferenceID?ReferenceID={1}", URL, filter.Data.ReferenceID);
            else if (!string.IsNullOrEmpty(filter.Data.StandardReferenceID))
                url = string.Format("{0}AppStandardReferenceItem/GetNewItemID?StandardReferenceID={1}", URL, filter.Data.StandardReferenceID);
            else if (!string.IsNullOrEmpty(filter.Data.TransType))
            {
                switch (filter.Data.TransType)
                {
                    case "IN":
                        url = string.Format("{0}Transaction/GetNewTransactionNo?TransType={1}", URL, filter.Data.TransType);
                        break;

                    case "OU":
                        url = string.Format("{0}Transaction/GetNewTransactionNo?TransType={1}", URL, filter.Data.TransType);
                        break;

                    case "DOC":
                        if (string.IsNullOrEmpty(filter.Data.PersonID))
                            return data = new Data.Root<string>
                            {
                                Succeeded = false,
                                Message = $"PersonID Is Empty"
                            };
                        url = string.Format("{0}UserDocument/GetNewDocumentID?TransType={1}&PersonID={2}", URL, filter.Data.TransType, filter.Data.PersonID);
                        break;

                    case "PDF":
                        if (string.IsNullOrEmpty(filter.Data.PersonID))
                            return data = new Data.Root<string>
                            {
                                Succeeded = false,
                                Message = $"PersonID Is Empty"
                            };
                        url = string.Format("{0}UserDocument/GetNewDocumentID?TransType={1}&PersonID={2}", URL, filter.Data.TransType, filter.Data.PersonID);
                        break;

                    case "UPL":
                        url = string.Format("{0}UserPicture/GetNewPictureID?TransType={1}", URL, filter.Data.TransType);
                        break;

                    case "RPT":
                        url = string.Format("{0}UserReport/GetNewReportNo?TransType={1}", URL, filter.Data.TransType);
                        break;

                    case "WSL":
                        url = string.Format("{0}UserWishlist/GetNewUserWishlistID?TransType={1}", URL, filter.Data.TransType);
                        break;

                    default:
                        url = string.Empty;
                        break;
                }
            }
            else
                url = string.Empty;

            if (string.IsNullOrEmpty(url))
                return data = new Data.Root<string>
                {
                    Succeeded = false,
                    Message = $"URL Is Empty"
                };

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
