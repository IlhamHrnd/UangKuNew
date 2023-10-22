using Newtonsoft.Json;

namespace UangKu.Model.Response.Location
{
    public class Provinces
    {
        public class ProvincesRoot
        {
            [JsonProperty("provID")]
            public int? provID { get; set; }

            [JsonProperty("provName")]
            public string provName { get; set; }

            [JsonProperty("locationID")]
            public int? locationID { get; set; }

            [JsonProperty("status")]
            public int? status { get; set; }
        }
    }
}
