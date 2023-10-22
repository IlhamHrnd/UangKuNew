using Newtonsoft.Json;

namespace UangKu.Model.Response.Location
{
    public class PostalCode
    {
        public class PostalCodeRoot
        {
            [JsonProperty("postalID")]
            public int? postalID { get; set; }

            [JsonProperty("subdisID")]
            public int? subdisID { get; set; }

            [JsonProperty("disID")]
            public int? disID { get; set; }

            [JsonProperty("cityID")]
            public int? cityID { get; set; }

            [JsonProperty("provID")]
            public int? provID { get; set; }

            [JsonProperty("postalCode")]
            public int? postalCode { get; set; }
        }
    }
}
