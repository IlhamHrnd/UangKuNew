using Newtonsoft.Json;

namespace UangKu.WebService.Data
{
    public class AppParameter
    {
        public class Data
        {
            [JsonProperty("parameterId")]
            public string parameterId { get; set; }

            [JsonProperty("parameterName")]
            public string parameterName { get; set; }

            [JsonProperty("parameterValue")]
            public string parameterValue { get; set; }

            [JsonProperty("srcontrol")]
            public string srcontrol { get; set; }

            [JsonProperty("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonProperty("lastUpdateByUserId")]
            public string lastUpdateByUserId { get; set; }

            [JsonProperty("isUsedBySystem")]
            public bool? isUsedBySystem { get; set; }
        }
    }
}
