using Newtonsoft.Json;

namespace UangKu.Model.Index.Body
{
    public class PostReport
    {
        [JsonProperty("reportNo")]
        public string reportNo { get; set; }

        [JsonProperty("dateErrorOccured")]
        public DateTime? dateErrorOccured { get; set; }

        [JsonProperty("srErrorLocation")]
        public string srErrorLocation { get; set; }

        [JsonProperty("srErrorPossibility")]
        public string srErrorPossibility { get; set; }

        [JsonProperty("errorCronologic")]
        public string errorCronologic { get; set; }

        [JsonProperty("picture")]
        public string picture { get; set; }

        [JsonProperty("createdDateTime")]
        public DateTime? createdDateTime { get; set; }

        [JsonProperty("createdByUserID")]
        public string createdByUserID { get; set; }

        [JsonProperty("lastUpdateDateTime")]
        public DateTime? lastUpdateDateTime { get; set; }

        [JsonProperty("lastUpdateByUserID")]
        public string lastUpdateByUserID { get; set; }

        [JsonProperty("personID")]
        public string personID { get; set; }
    }
}
