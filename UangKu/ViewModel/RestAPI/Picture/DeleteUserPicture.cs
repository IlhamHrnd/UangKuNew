using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Picture
{
    public class DeleteUserPicture : BaseModel
    {
        private const string DeleteUserPictureEndPoint = "{1}UserPicture/DeleteUserPicture?pictureID={0}";

        public static async Task<string> DeleteUserPictureID(string pictureID, Model.Index.Body.DeleteUserPicture picture)
        {
            string result;
            string pictureMsg = string.Empty;
            string url = string.Format(DeleteUserPictureEndPoint, pictureID, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Delete,
                Timeout = TimeOut
            };
            var body = new Model.Index.Body.DeleteUserPicture
            {
                lastUpdateUserID = picture.lastUpdateUserID,
                isDeleted = picture.isDeleted
            };
            request.AddJsonBody(body);

            var response = await client.ExecuteAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.Substring(1, response.Content.Length - 2);
                }
                else
                {
                    result = response.Content;
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}
