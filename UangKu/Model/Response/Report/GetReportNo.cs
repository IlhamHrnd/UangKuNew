using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.Report
{
    public class GetReportNoRoot : IResponse
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

        [JsonProperty("isApprove")]
        public bool? isApprove { get; set; }

        [JsonProperty("srReportStatus")]
        public string srReportStatus { get; set; }

        [JsonProperty("approvedDateTime")]
        public DateTime? approvedDateTime { get; set; }

        [JsonProperty("approvedByUserID")]
        public string approvedByUserID { get; set; }

        [JsonProperty("voidDateTime")]
        public DateTime? voidDateTime { get; set; }

        [JsonProperty("voidByUserID")]
        public string voidByUserID { get; set; }

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
        public MetaData metaData { get; set; }
    }
}
