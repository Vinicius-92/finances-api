using System.Collections.Generic;

namespace FinancesAPI.Models
{
    public class Wallet
    {
        public Wallet()
        {
        }

        public Wallet(int id, decimal nativeCurrency, int userId)
        {
            Id = id;
            NativeCurrency = nativeCurrency;
            UserId = userId;
        }

        public int Id { get; set; }
        public decimal NativeCurrency { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public IList<CurrencyInvestiments> CurrencyInvestiments { get; set; }

        public void AddValueToNativeCurrencyInWallet(decimal value) => 
            this.NativeCurrency += value;
    }
}