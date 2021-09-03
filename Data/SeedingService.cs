using System.Linq;
using FinancesAPI.Models;

namespace FinancesAPI.Data
{
    public class SeedingService
    {
        private readonly ApplicationDbContext _context;

        public SeedingService(ApplicationDbContext context) => 
                _context = context;

        public void Seed()
        {
            if(_context.Currencies.Any() || _context.Users.Any() || _context.Wallets.Any() || _context.CurrencyInvestiments.Any())
                return;
            var USD = new Currency { Code = "USD", Name = "Dólar" };
            var CAD = new Currency { Code = "CAD", Name = "Dólar Canadense" };
            var GBP = new Currency { Code = "GBP", Name = "Libra Esterlina" };
            var ARS = new Currency { Code = "ARS", Name = "Peso Argentino" };
            var BTC = new Currency { Code = "BTC", Name = "Bitcoin" };
            var EUR = new Currency { Code = "EUR", Name = "Euro" };
            var JPY = new Currency { Code = "JPY", Name = "Iene Japonês" };
            var CHF = new Currency { Code = "CHF", Name = "Franco Suíço" };
            var AUD = new Currency { Code = "AUD", Name = "Dólar Australiano" };
            var CNY = new Currency { Code = "CNY", Name = "Yuan Chinês" };
            var ILS = new Currency { Code = "ILS", Name = "Novo Shekel Israelense" };
            var ETH = new Currency { Code = "ETH", Name = "Ethereum" };
            var XRP = new Currency { Code = "XRP", Name = "XRP" };
            var DOGE = new Currency { Code = "DOGE", Name = "Dogecoin" };
            _context.Currencies.AddRange(USD, CAD, GBP, ARS, BTC, EUR, JPY, CHF, AUD, CNY, ILS, ETH, XRP, DOGE);

            var p1 = new User(1, "Vinicius", "vinicius@gft.com", "vinicius");
            var p2 = new User(2, "Clecio", "clecio@gft.com", "clecio");
            var p3 = new User(3, "David", "heloisa@gft.com", "david");
            var p4 = new User(4, "Alessandro", "alessandro@gft.com", "alessandro");
            _context.Users.AddRange(p1, p2, p3, p4);

            var w1 = new Wallet(1, 50000, 1);
            var w2 = new Wallet(2, 5050000, 2);
            var w3 = new Wallet(3, 10000, 3);
            var w4 = new Wallet(4, 10000000, 4);
            _context.Wallets.AddRange(w1, w2, w3, w4);

            var cy11 = new CurrencyInvestiments(1, "USD", 5000, w1);
            var cy12 = new CurrencyInvestiments(2, "CAD", 15000, w1);
            var cy13 = new CurrencyInvestiments(3, "BTC", 20, w1);
            var cy14 = new CurrencyInvestiments(4, "ARS", 255000, w1);
            var cy21 = new CurrencyInvestiments(5, "USD", 55000, w2);
            var cy22 = new CurrencyInvestiments(6, "EUR", 9000, w2);
            var cy23 = new CurrencyInvestiments(7, "DOGE", 1000, w2);
            var cy24 = new CurrencyInvestiments(8, "ARS", 15000, w2);
            var cy31 = new CurrencyInvestiments(9, "USD", 1500, w3);
            var cy32 = new CurrencyInvestiments(10, "CAD", 100, w3);
            var cy33 = new CurrencyInvestiments(11, "ARS", 569020, w3);
            var cy34 = new CurrencyInvestiments(12, "JPY", 255000, w3);
            var cy41 = new CurrencyInvestiments(13, "JPY", 555000, w4);
            var cy42 = new CurrencyInvestiments(14, "USD", 15000, w4);
            var cy43 = new CurrencyInvestiments(15, "DOGE", 1500, w4);
            var cy44 = new CurrencyInvestiments(16, "EUR", 3300, w4);
            _context.CurrencyInvestiments.AddRange(cy11, cy12, cy13, cy14, cy21, 
                                                    cy22, cy23, cy24, cy31, cy32, 
                                                    cy33, cy34, cy41, cy42, cy43, cy44);
            _context.SaveChanges();
        }
    }
}