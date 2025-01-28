using RestSharp;
using UangKu.Model.Base;

namespace UangKu.ViewModel.RestAPI.Picture
{
    public class PostUserPicture : BaseModel
    {
        private const string PostUserPictureEndPoint = "{0}UserPicture/PostUserPicture";

        public static async Task<string> PostNewUserPicture(Model.Index.Body.PostPicture picture)
        {
            string result;
            string UserPicture = string.Empty;
            string url = string.Format(PostUserPictureEndPoint, URL);
            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(TimeOut)
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
