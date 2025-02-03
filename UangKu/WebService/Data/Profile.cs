using System.Text.Json.Serialization;

namespace UangKu.WebService.Data
{
    public class Profile
    {
        public class Data
        {
            [JsonPropertyName("personId")]
            public string personId { get; set; }

            [JsonPropertyName("firstName")]
            public string firstName { get; set; }

            [JsonPropertyName("middleName")]
            public string middleName { get; set; }

            [JsonPropertyName("lastName")]
            public string lastName { get; set; }

            [JsonPropertyName("birthDate")]
            public DateTime? birthDate { get; set; }

            [JsonPropertyName("placeOfBirth")]
            public string placeOfBirth { get; set; }

            [JsonPropertyName("photo")]
            public string photo { get; set; }

            [JsonPropertyName("address")]
            public string address { get; set; }

            [JsonPropertyName("province")]
            public string province { get; set; }

            [JsonPropertyName("city")]
            public string city { get; set; }

            [JsonPropertyName("subdistrict")]
            public string subdistrict { get; set; }

            [JsonPropertyName("district")]
            public string district { get; set; }

            [JsonPropertyName("postalCode")]
            public int? postalCode { get; set; }

            [JsonPropertyName("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonPropertyName("lastUpdateByUser")]
            public string lastUpdateByUser { get; set; }

            #region Custom Variabel
            public ImageSource source { get; set; }
            public string dateFormat { get; set; }
            public string ageFormat { get; set; }
            #endregion
        }
    }
}
