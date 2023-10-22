using Newtonsoft.Json;

namespace UangKu.Model.Response.Location
{
    public class District
    {
        public class DistrictRoot
        {
            [JsonProperty("disID")]
            public int? disID { get; set; }

            [JsonProperty("disName")]
            public string disName { get; set; }

            [JsonProperty("cityID")]
            public int? cityID { get; set; }
        }
    }
}
