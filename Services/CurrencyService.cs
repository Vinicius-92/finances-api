using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinancesAPI.Data;
using FinancesAPI.Models;
using FinancesAPI.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FinancesAPI.Services
{
    public class CurrencyService
    {
        private readonly ApplicationDbContext _context;
        public CurrencyService(ApplicationDbContext context) => 
                _context = context;

        public async Task<List<Currency>> ReturnAllCurrenciesAsync() => 
                await _context.Currencies.ToListAsync();

        public async Task<Currency> CreateNewCurrencyAsync(Currency currency)
        {
            try
            {
                _context.Currencies.Add(currency);
                await _context.SaveChangesAsync();
                return currency;
            }
            catch(Exception e)
            {
                throw new DatabaseIntegrityException(e.Message);
            }
        }

        public async Task DeleteCurrencyAsync(Currency currency)
        {
            try
            {
                _context.Currencies.Remove(currency);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new DatabaseIntegrityException(e.Message);
            }
        }
    }
}