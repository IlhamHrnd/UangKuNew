using Newtonsoft.Json;

namespace UangKu.Model.Index.Body
{
    public class DeleteUserPicture
    {
        [JsonProperty("lastUpdateUserID")]
        public string lastUpdateUserID { get; set; }

        [JsonProperty("isDeleted")]
        public bool? isDeleted { get; set; }
    }
}
