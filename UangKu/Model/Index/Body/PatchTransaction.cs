using Newtonsoft.Json;

namespace UangKu.Model.Index.Body
{
    public class PatchTransaction
    {
        [JsonProperty("transNo")]
        public string transNo { get; set; }

        [JsonProperty("srTransaction")]
        public string srTransaction { get; set; }

        [JsonProperty("srTransItem")]
        public string srTransItem { get; set; }

        [JsonProperty("amount")]
        public decimal? amount { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("photo")]
        public string photo { get; set; }

        [JsonProperty("lastUpdateDateTime")]
        public DateTime? lastUpdateDateTime { get; set; }

        [JsonProperty("lastUpdateByUserID")]
        public string lastUpdateByUserID { get; set; }

        [JsonProperty("transType")]
        public string transType { get; set; }

        [JsonProperty("transDate")]
        public string transDate { get; set; }
    }
}
