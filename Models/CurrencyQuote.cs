using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace FinancesAPI.Models
{
    public class CurrencyQuote
    {
        [Key]
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("codein")]
        public string CodeIn { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("high")]
        public string High { get; set; }

        [JsonProperty("low")]
        public string Low { get; set; }

        [JsonProperty("varBid")]
        public string Variation { get; set; }

        [JsonProperty("pctChange")]
        public string VariationPercentage { get; set; }

        [JsonProperty("bid")]
        public string PurchaseBid { get; set; }

        [JsonProperty("ask")]
        public string SellingPrice { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("create_date")]
        public string CreateDate { get; set; }

    }
}