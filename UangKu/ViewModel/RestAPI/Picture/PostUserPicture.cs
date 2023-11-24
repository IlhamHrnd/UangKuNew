using Newtonsoft.Json;
using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Picture
{
    public class PostUserPicture
    {
        private const string PostUserPictureEndPoint = "https://uangkuapi.azurewebsites.net/UserPicture/PostUserPicture";

        public static async Task<string> PostNewUserPicture(Model.Index.Body.PostPicture picture)
        {
            string UserPicture = string.Empty;
            string url = string.Format(PostUserPictureEndPoint);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = ParameterModel.ItemDefaultValue.Timeout
            };
            var body = new Model.Index.Body.PostPicture
            {
                pictureID = picture.pictureID,
                picture = picture.picture,
                pictureName = picture.pictureName,
                pictureFormat = picture.pictureFormat,
                personID = picture.personID,
                isDeleted = picture.isDeleted,
                createdByUserID = picture.createdByUserID,
                createdDateTime = picture.createdDateTime,
                lastUpdateDateTime = picture.lastUpdateDateTime,
                lastUpdateByUserID = picture.lastUpdateByUserID,
                pictureSize = picture.pictureSize
            };
            request.AddJsonBody(body);

            var response = await client.ExecutePostAsync(request);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var post = JsonConvert.DeserializeObject<string>(content);
                }
                else
                {
                    await MsgModel.MsgNotification(response.ErrorMessage);
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
