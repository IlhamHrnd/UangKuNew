using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.User
{
    public class UsernameRoot : IResponse
    {
        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("sexName")]
        public string sexName { get; set; }

        [JsonProperty("accessName")]
        public string accessName { get; set; }

        [JsonProperty("statusName")]
        public string statusName { get; set; }

        [JsonProperty("activeDate")]
        public DateTime? activeDate { get; set; }

        [JsonProperty("lastLogin")]
        public DateTime? lastLogin { get; set; }

        [JsonProperty("lastUpdateDateTime")]
        public DateTime? lastUpdateDateTime { get; set; }

        [JsonProperty("lastUpdateByUser")]
        public string lastUpdateByUser { get; set; }

        [JsonProperty("personID")]
        public string personID { get; set; }
        public string imgavatar { get; set; }
        public MetaData metaData { get; set; }
    }
}
