namespace FinancesAPI.Models.ResponseModels
{
    public class CurrencyExchangeResponse
    {
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public string Action { get; set; }
        public decimal ValueFrom { get; set; }
        public decimal ValueTo { get; set; }
    }
}