using Newtonsoft.Json;

namespace UangKu.Model.Response.AppStandardReferenceItem
{
    public class AppStandardReferenceItem
    {
        public class AsriRoot
        {
            [JsonProperty("standardReferenceID")]
            public string standardReferenceID { get; set; }

            [JsonProperty("itemID")]
            public string itemID { get; set; }

            [JsonProperty("itemName")]
            public string itemName { get; set; }

            [JsonProperty("note")]
            public string note { get; set; }

            [JsonProperty("isUsedBySystem")]
            public bool? isUsedBySystem { get; set; }

            [JsonProperty("isActive")]
            public bool? isActive { get; set; }

            [JsonProperty("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonProperty("lastUpdateByUserID")]
            public string lastUpdateByUserID { get; set; }
        }

        public class AsriTwoRoot
        {
            [JsonProperty("standardReferenceID")]
            public string standardReferenceID { get; set; }

            [JsonProperty("itemID")]
            public string itemID { get; set; }

            [JsonProperty("itemName")]
            public string itemName { get; set; }

            [JsonProperty("note")]
            public string note { get; set; }

            [JsonProperty("isUsedBySystem")]
            public bool? isUsedBySystem { get; set; }

            [JsonProperty("isActive")]
            public bool? isActive { get; set; }

            [JsonProperty("lastUpdateDateTime")]
            public DateTime? lastUpdateDateTime { get; set; }

            [JsonProperty("lastUpdateByUserID")]
            public string lastUpdateByUserID { get; set; }
        }
    }
}
