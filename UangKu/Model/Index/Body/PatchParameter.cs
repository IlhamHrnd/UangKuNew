﻿using Newtonsoft.Json;

namespace UangKu.Model.Index.Body
{
    public class PatchParameter
    {
        [JsonProperty("parameterID")]
        public string parameterID { get; set; }

        [JsonProperty("parameterName")]
        public string parameterName { get; set; }

        [JsonProperty("parameterValue")]
        public string parameterValue { get; set; }

        [JsonProperty("srControl")]
        public string srControl { get; set; }

        [JsonProperty("lastUpdateDateTime")]
        public DateTime? lastUpdateDateTime { get; set; }

        [JsonProperty("lastUpdateByUserID")]
        public string lastUpdateByUserID { get; set; }

        [JsonProperty("isUsedBySystem")]
        public bool? isUsedBySystem { get; set; }
    }
}
