using Newtonsoft.Json;

namespace UangKu.WebService.Data
{
    public class User
    {
        public class Data
        {
            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("password")]
            public object Password { get; set; }

            [JsonProperty("srsex")]
            public string Srsex { get; set; }

            [JsonProperty("sraccess")]
            public string Sraccess { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("srstatus")]
            public string Srstatus { get; set; }

            [JsonProperty("activeDate")]
            public DateTime? ActiveDate { get; set; }

            [JsonProperty("lastLogin")]
            public DateTime? LastLogin { get; set; }

            [JsonProperty("lastUpdateDateTime")]
            public DateTime? LastUpdateDateTime { get; set; }

            [JsonProperty("lastUpdateByUser")]
            public string LastUpdateByUser { get; set; }

            [JsonProperty("personId")]
            public string PersonId { get; set; }
            #region Untuk Model Styling
            public string imgavatar { get; set; }
            public string dateActive { get; set; }
            public string dateLogin { get; set; }
            public bool isActive { get; set; }
            #endregion
        }
    }
}
