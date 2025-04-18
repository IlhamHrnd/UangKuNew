﻿using Newtonsoft.Json;
using UangKu.Model.Base;

namespace UangKu.Model.Response.Transaction
{
    public class GetAllPDFTransaction
    {
        public class PDFTransactionRoot : IResponse
        {
            public List<Datum> data { get; set; }
            public MetaData metaData { get; set; }
        }

        public class Datum
        {
            [JsonProperty("transNo")]
            public string transNo { get; set; }

            [JsonProperty("srTransaction")]
            public string srTransaction { get; set; }

            [JsonProperty("srTransItem")]
            public string srTransItem { get; set; }

            [JsonProperty("amount")]
            public double? amount { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }

            [JsonProperty("photo")]
            public string photo { get; set; }

            [JsonProperty("transType")]
            public string transType { get; set; }

            [JsonProperty("personID")]
            public string personID { get; set; }

            [JsonProperty("transDate")]
            public DateTime? transDate { get; set; }
        }
    }
}
