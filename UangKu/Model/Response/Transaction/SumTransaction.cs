using System.Text.Json.Serialization;
using UangKu.Model.Base;

namespace UangKu.Model.Response.Transaction
{
    public class SumTransaction
    {
        public class SumTransactionRoot : IResponse
        {
            public List<Datum> data { get; set; }
            public MetaData metaData { get; set; }
        }

        public class Datum
        {
            [JsonPropertyName("amount")]
            public decimal? amount { get; set; }

            [JsonPropertyName("srTransaction")]
            public string srTransaction { get; set; }
            public string amountFormat { get; set; }
        }
    }
}
