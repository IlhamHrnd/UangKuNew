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
            public object programName { get; set; }

            [JsonProperty("note")]
            public object note { get; set; }

            [JsonProperty("isProgram")]
            public object isProgram { get; set; }

            [JsonProperty("isProgramAddAble")]
            public object isProgramAddAble { get; set; }

            [JsonProperty("isProgramEditAble")]
            public object isProgramEditAble { get; set; }

            [JsonProperty("isProgramDeleteAble")]
            public object isProgramDeleteAble { get; set; }

            [JsonProperty("isProgramViewAble")]
            public object isProgramViewAble { get; set; }

            [JsonProperty("isProgramApprovalAble")]
            public object isProgramApprovalAble { get; set; }

            [JsonProperty("isProgramUnApprovalAble")]
            public object isProgramUnApprovalAble { get; set; }

            [JsonProperty("isProgramVoidAble")]
            public object isProgramVoidAble { get; set; }

            [JsonProperty("isProgramUnVoidAble")]
            public object isProgramUnVoidAble { get; set; }

            [JsonProperty("isProgramPrintAble")]
            public object isProgramPrintAble { get; set; }

            [JsonProperty("isVisible")]
            public object isVisible { get; set; }

            [JsonProperty("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonProperty("lastUpdateByUserId")]
            public string lastUpdateByUserId { get; set; }

            [JsonProperty("isUsedBySystem")]
            public int? isUsedBySystem { get; set; }
        }
    }
}
