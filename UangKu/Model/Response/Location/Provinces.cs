using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.Location
{
    public class Provinces
    {
        public class ProvincesRoot : IResponse
        {
            public List<Datum> data { get; set; }
            public MetaData metaData { get; set; }
        }

        public class Datum
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
