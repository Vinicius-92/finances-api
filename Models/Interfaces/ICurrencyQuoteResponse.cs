using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace FinancesAPI.Models.interfaces
{
    public interface ICurrencyQuoteResponse
    {
        [Get("/{code}-BRL")]
        Task<CurrencyQuote[]> GetCurrencyQuoteByCodeAsync(string code);
    }
}