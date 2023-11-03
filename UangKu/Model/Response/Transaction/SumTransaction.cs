using System.Text.Json.Serialization;

namespace UangKu.Model.Response.Transaction
{
    public class SumTransaction
    {
        public class SumTransactionRoot
        {
            [JsonPropertyName("amount")]
            public decimal? amount { get; set; }

            [JsonPropertyName("srTransaction")]
            public string srTransaction { get; set; }
            public string amountFormat { get; set; }
        }
    }
}
