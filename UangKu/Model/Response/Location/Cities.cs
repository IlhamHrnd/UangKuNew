using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.Location
{
    public class Cities
    {
        public class CitiesRoot : IResponse
        {
            public List<Datum> data { get; set; }
            public MetaData metaData { get; set; }
        }

        public class Datum
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
