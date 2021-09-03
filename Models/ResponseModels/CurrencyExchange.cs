using Newtonsoft.Json;

namespace FinancesAPI.Models.ResponseModels
{
    public class CurrencyExchange
    {
        [JsonProperty("buycurrency")]
        public decimal BuyCurrency { get; set; }
        [JsonProperty("sellcurrency")]
        public decimal SellCurrency { get; set; }
    }
}