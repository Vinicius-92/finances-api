using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using FinancesAPI.Data;
using FinancesAPI.Models;
using FinancesAPI.Models.Exceptions;
using FinancesAPI.Models.interfaces;
using FinancesAPI.Models.Interfaces;
using FinancesAPI.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Refit;

namespace FinancesAPI.Services
{
    public class CurrencyQuoteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ICurrencyQuoteResponse quoteResponse = RestService.For<ICurrencyQuoteResponse>(IApiUrl.Uri);
        public CurrencyQuoteService(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<List<Currency>> ReturnAllCurrenciesAsync() => 
            await _context.Currencies.ToListAsync();

        public async Task<CurrencyQuote> GetCurrencyAsync(string code)
        {
            var currency = await quoteResponse.GetCurrencyQuoteByCodeAsync(code);
            return currency[0];
        }

        public async Task<decimal> ExchangeCurrencyAsync(string code, CurrencyExchange exchange)
        {
            var currencyQuote = await quoteResponse.GetCurrencyQuoteByCodeAsync(code);
            if(currencyQuote == null)
                return 0;
            if(exchange.SellCurrency != 0)
                return exchange.SellCurrency * decimal.Parse(currencyQuote[0].SellingPrice, CultureInfo.InvariantCulture);
            else if(exchange.BuyCurrency != 0)
                return exchange.BuyCurrency / decimal.Parse(currencyQuote[0].PurchaseBid, CultureInfo.InvariantCulture);
            else
                return 0;
        }

        public async Task<bool> Buy(CurrencyExchangeResponse response, int userId)
        {
            var user = await _context.Users.Include(x => x.MyWallet).Include(p => p.MyWallet.CurrencyInvestiments).FirstAsync(y => y.Id == userId);
            var investiment = user.MyWallet.CurrencyInvestiments.FirstOrDefault(p => p.CurrencyCode == response.CurrencyTo);
            if(user.MyWallet.NativeCurrency < response.ValueFrom)
                return false;
            if(investiment != null && investiment.CurrencyCode == response.CurrencyTo)
            {
                investiment.Value += response.ValueTo;
                user.MyWallet.NativeCurrency -= response.ValueFrom;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                var investimentToSave = new CurrencyInvestiments {
                    CurrencyCode = response.CurrencyTo,
                    Value = response.ValueTo,
                };
                user.MyWallet.NativeCurrency -= response.ValueFrom;
                user.MyWallet.CurrencyInvestiments.Add(investimentToSave);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> Sell(CurrencyExchangeResponse response, int userId)
        {
            var user = await _userService.ReturnUser(userId);
            var investiment = user.MyWallet.CurrencyInvestiments.FirstOrDefault(p => p.CurrencyCode == response.CurrencyFrom);
            if(investiment != null)
            {
                if(investiment.Value < response.ValueFrom)
                    return false;
                investiment.Value -= response.ValueFrom;
                user.MyWallet.NativeCurrency += response.ValueTo;
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}