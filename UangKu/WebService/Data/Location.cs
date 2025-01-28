using System.Text.Json.Serialization;

namespace UangKu.WebService.Data
{
    public class Location
    {
        public class Province
        {
            [JsonPropertyName("provId")]
            public int? provId { get; set; }

            [JsonPropertyName("provName")]
            public string provName { get; set; }

            [JsonPropertyName("locationId")]
            public int? locationId { get; set; }

            [JsonPropertyName("status")]
            public int? status { get; set; }
        }

        public class City
        {
            [JsonPropertyName("cityId")]
            public int? cityId { get; set; }

            [JsonPropertyName("cityName")]
            public string cityName { get; set; }

            [JsonPropertyName("provId")]
            public int? provId { get; set; }
        }

        public class District
        {
            [JsonPropertyName("disId")]
            public int? disId { get; set; }

            [JsonPropertyName("disName")]
            public string disName { get; set; }

            [JsonPropertyName("cityId")]
            public int? cityId { get; set; }
        }

        public class SubDistrict
        {
            [JsonPropertyName("subdisId")]
            public int? subdisId { get; set; }

            [JsonPropertyName("subdisName")]
            public string subdisName { get; set; }

            [JsonPropertyName("disId")]
            public int? disId { get; set; }
        }

        public class PostalCode
        {
            [JsonPropertyName("postalId")]
            public int? postalId { get; set; }

            [JsonPropertyName("subdisId")]
            public int? subdisId { get; set; }

            [JsonPropertyName("disId")]
            public int? disId { get; set; }

            [JsonPropertyName("cityId")]
            public int? cityId { get; set; }

            [JsonPropertyName("provId")]
            public int? provId { get; set; }

            [JsonPropertyName("postalCode1")]
            public int? postalCode1 { get; set; }
        }
    }
}
