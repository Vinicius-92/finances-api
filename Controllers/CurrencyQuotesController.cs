using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinancesAPI.Services;
using System;
using FinancesAPI.Models.ResponseModels;
using FinancesAPI.Hateoas;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace FinancesAPI.Controllers
{
    [ApiController]
    [Route("/details")]
    public class CurrencyQuotesController : ControllerBase
    {
        private readonly CurrencyQuoteService _currencyQuoteService;
        private HateoasImplementation _hateoas;
        public CurrencyQuotesController(CurrencyQuoteService currencyQuoteService)
        {
            _currencyQuoteService = currencyQuoteService;
            _hateoas = new HateoasImplementation("localhost:5001/");
            _hateoas.AddAction("EXCHANGE", "POST");
        }

        [HttpGet]
        [Route("/details/{code}")]
        [Authorize]
        public async Task<IActionResult> GetCurrenciesAsync(string code)
        {
            try
            {
                var currency = await _currencyQuoteService.GetCurrencyAsync(code);
                var currencyToReturn = new CurrencyQuoteContainer { CurrencyQuote = currency, Links = _hateoas.GetActions(code) };
                return Ok(currencyToReturn);
            } catch (Exception)
            {
                return NotFound($"Currency with {code} not found");
            }
        }

        [HttpPost]
        [Route("/details/{code}")]
        [Authorize]
        public async Task<IActionResult> ExchangeCurrencyAsync(string code, [FromBody] CurrencyExchange exchange)
        {
                var userId = Int32.Parse(HttpContext.User.Claims.First(c => c.Type.ToString().Equals("id", StringComparison.InvariantCultureIgnoreCase)).Value);
                var returned = await _currencyQuoteService.ExchangeCurrencyAsync(code, exchange);
                if(exchange.SellCurrency != 0)
                {
                    var returnFromSell = new CurrencyExchangeResponse{ 
                            CurrencyFrom = code,
                            CurrencyTo = "BRL",
                            Action = "Sell",
                            ValueFrom = Math.Round(exchange.SellCurrency, 4),
                            ValueTo = Math.Round(returned, 4) };
                    await _currencyQuoteService.Sell(returnFromSell, userId);
                    return Ok(returnFromSell);
                }
                if(exchange.BuyCurrency != 0)
                {
                    var returnFromBuy = new CurrencyExchangeResponse{ 
                            CurrencyFrom = "BRL",
                            CurrencyTo = code,
                            Action = "Buy",
                            ValueFrom = Math.Round(exchange.BuyCurrency, 4),
                            ValueTo = Math.Round(returned, 4) };
                    await _currencyQuoteService.Buy(returnFromBuy, userId);
                    return Ok(returnFromBuy);
                }
                return BadRequest();
        }
    }
}