using Newtonsoft.Json;

namespace UangKu.WebService.Data
{
    public class AppProgram
    {
        public class Data
        {
            [JsonProperty("programId")]
            public string programId { get; set; }

            [JsonProperty("programName")]
            public string programName { get; set; }

            [JsonProperty("note")]
            public string note { get; set; }

            [JsonProperty("isProgram")]
            public bool isProgram { get; set; }

            [JsonProperty("isProgramAddAble")]
            public bool? isProgramAddAble { get; set; }

            [JsonProperty("isProgramEditAble")]
            public bool? isProgramEditAble { get; set; }

            [JsonProperty("isProgramDeleteAble")]
            public bool? isProgramDeleteAble { get; set; }

            [JsonProperty("isProgramViewAble")]
            public bool? isProgramViewAble { get; set; }

            [JsonProperty("isProgramApprovalAble")]
            public bool? isProgramApprovalAble { get; set; }

            [JsonProperty("isProgramUnApprovalAble")]
            public bool? isProgramUnApprovalAble { get; set; }

            [JsonProperty("isProgramVoidAble")]
            public bool? isProgramVoidAble { get; set; }

            [JsonProperty("isProgramUnVoidAble")]
            public bool? isProgramUnVoidAble { get; set; }

            [JsonProperty("isProgramPrintAble")]
            public bool? isProgramPrintAble { get; set; }

            [JsonProperty("isVisible")]
            public bool? isVisible { get; set; }

            [JsonProperty("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonProperty("lastUpdateByUserId")]
            public string lastUpdateByUserId { get; set; }

            [JsonProperty("isUsedBySystem")]
            public bool? isUsedBySystem { get; set; }
        }
    }
}
