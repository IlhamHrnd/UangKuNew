using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.Location
{
    public class District
    {
        public class DistrictRoot : IResponse
        {
            public List<Datum> data { get; set; }
            public MetaData metaData { get; set; }
        }

        public class Datum
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
