using System.Collections.Generic;
using System.Threading.Tasks;
using FinancesAPI.Models;

namespace FinancesAPI.Data
{
    public interface ICurrencyService
    {
         Task DeleteCurrencyAsync(Currency currency);
         Task<Currency> CreateNewCurrencyAsync(Currency currency);
         Task<List<Currency>> ReturnAllCurrenciesAsync();
         
    }
}