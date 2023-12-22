using Newtonsoft.Json;

namespace UangKu.Model.Response.Report
{
    public class Report
    {
        public class Datum
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

            [JsonProperty("createdDateTime")]
            public DateTime? createdDateTime { get; set; }

            [JsonProperty("createdByUserID")]
            public string createdByUserID { get; set; }

            [JsonProperty("personID")]
            public string personID { get; set; }
            public bool isvisible { get; set; }
            public string dateError { get; set; }
            public string dateCreated { get; set; }
            public ImageSource source { get; set; }
        }

        public class ReportRoot
        {
            [JsonProperty("pageNumber")]
            public int? pageNumber { get; set; }

            [JsonProperty("pageSize")]
            public int? pageSize { get; set; }

            [JsonProperty("totalPages")]
            public int? totalPages { get; set; }

            [JsonProperty("totalRecords")]
            public int? totalRecords { get; set; }

            [JsonProperty("prevPageLink")]
            public object prevPageLink { get; set; }

            [JsonProperty("nextPageLink")]
            public object nextPageLink { get; set; }

            [JsonProperty("data")]
            public List<Datum> data { get; set; }

            [JsonProperty("succeeded")]
            public bool? succeeded { get; set; }

            [JsonProperty("errors")]
            public object errors { get; set; }

            [JsonProperty("message")]
            public object message { get; set; }
        }
    }
}
