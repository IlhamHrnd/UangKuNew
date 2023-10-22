using Newtonsoft.Json;

namespace UangKu.Model.Response.Location
{
    public class Cities
    {
        public class CitiesRoot
        {
            [JsonProperty("cityID")]
            public int? cityID { get; set; }

            [JsonProperty("cityName")]
            public string cityName { get; set; }

            [JsonProperty("provID")]
            public int? provID { get; set; }
        }
    }
}
