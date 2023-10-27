using Newtonsoft.Json;

namespace UangKu.Model.Response.Profile
{
    public class GetProfile
    {
        public class GetProfileRoot
        {
            [JsonProperty("personID")]
            public string personID { get; set; }

            [JsonProperty("firstName")]
            public string firstName { get; set; }

            [JsonProperty("middleName")]
            public string middleName { get; set; }

            [JsonProperty("lastName")]
            public string lastName { get; set; }

            public string fullName
            {
                get
                {
                    return $"{firstName} {middleName} {lastName}";
                }
            }

            [JsonProperty("birthDate")]
            public DateTime? birthDate { get; set; }

            [JsonProperty("placeOfBirth")]
            public string placeOfBirth { get; set; }

            [JsonProperty("photo")]
            public string photo { get; set; }

            [JsonProperty("address")]
            public string address { get; set; }

            [JsonProperty("province")]
            public string province { get; set; }

            [JsonProperty("city")]
            public string city { get; set; }

            [JsonProperty("subdistrict")]
            public string subdistrict { get; set; }

            [JsonProperty("district")]
            public string district { get; set; }

            [JsonProperty("postalCode")]
            public int? postalCode { get; set; }

            [JsonProperty("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonProperty("lastUpdateByUser")]
            public string lastUpdateByUser { get; set; }
        }
    }
}
