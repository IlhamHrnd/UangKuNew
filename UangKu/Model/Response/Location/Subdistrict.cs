using Newtonsoft.Json;

namespace UangKu.Model.Response.Location
{
    public class Subdistrict
    {
        public class SubdistrictRoot
        {
            [JsonProperty("subdisID")]
            public int? subdisID { get; set; }

            [JsonProperty("subdisName")]
            public string subdisName { get; set; }

            [JsonProperty("disID")]
            public int? disID { get; set; }
        }
    }
}
