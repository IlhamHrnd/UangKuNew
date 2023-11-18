using Newtonsoft.Json;

namespace UangKu.Model.Index.Body
{
    public class PatchUsername
    {
        [JsonProperty("sex")]
        public string sex { get; set; }

        [JsonProperty("access")]
        public string access { get; set; }

        [JsonProperty("isActive")]
        public bool? isActive { get; set; }

        [JsonProperty("lastUpdateUser")]
        public string lastUpdateUser { get; set; }
    }
}
