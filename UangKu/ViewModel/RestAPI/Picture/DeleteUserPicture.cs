using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Picture
{
    public class DeleteUserPicture : BaseModel
    {
        private const string DeleteUserPictureEndPoint = "{1}UserPicture/DeleteUserPicture?pictureID={0}";

        public static async Task<string> DeleteUserPictureID(string pictureID, Model.Index.Body.DeleteUserPicture picture)
        {
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
                    var content = response.Content;
                    var delete = JsonConvert.DeserializeObject<string>(content);
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
            var format = response.Content.Substring(1, response.Content.Length - 2);

            return format;
        }
    }
}
