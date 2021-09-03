using System.Text.Json.Serialization;

namespace FinancesAPI.Models
{
    public class CurrencyInvestiments
    {
        public CurrencyInvestiments()
        {
        }
        public CurrencyInvestiments(int id, string currencyCode, decimal value, Wallet wallet)
        {
            Id = id;
            CurrencyCode = currencyCode;
            Value = value;
            Wallet = wallet;
        }

        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Value { get; set; }
        [JsonIgnore]
        public Wallet Wallet { get; set; }

        public override string ToString()
        {
            return Id + Value.ToString() + Wallet.Id;
        }
    }
}