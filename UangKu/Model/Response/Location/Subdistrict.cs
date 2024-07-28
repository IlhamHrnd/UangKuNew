using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.Location
{
    public class Subdistrict
    {
        public class SubdistrictRoot : IResponse
        {
            public List<Datum> data { get; set; }
            public MetaData metaData { get; set; }
        }

        public class Datum
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
