using System.Collections.Generic;

namespace FinancesAPI.Models.ResponseModels
{
    public class UserMyWalletResponse
    {
        public string Name { get; set; }
        public decimal NativeCurrency { get; set; }
         public IList<CurrencyInvestiments> CurrencyInvestiments { get; set; }
    }
}