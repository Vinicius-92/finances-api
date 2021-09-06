using System.Collections.Generic;
using System.Threading.Tasks;
using FinancesAPI.Data;
using FinancesAPI.Hateoas;
using FinancesAPI.Models;
using FinancesAPI.Models.ResponseModels;
using FinancesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancesAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private HateoasImplementation _hateoas;
        public CurrenciesController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
            _hateoas = new HateoasImplementation("localhost:5001/details/");
            _hateoas.AddAction("DETAILS", "GET");
            _hateoas.AddAction("EXCHANGE", "POST");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var listOfCurrencies = await _currencyService.ReturnAllCurrenciesAsync();
            var currenciesToReturnWithHateoas = new List<CurrencyContainer>();
            foreach (var item in listOfCurrencies)
            {
                currenciesToReturnWithHateoas.Add(new CurrencyContainer {
                    Currency = item,
                    Links = _hateoas.GetActions(item.Code)
                });
            }
            return Ok(currenciesToReturnWithHateoas);
        }

        [HttpPost]
        [Route("/currencies/create")]
        public async Task<IActionResult> CreateCurrency([FromBody] Currency currency)
        {
            var createdCurrency = await _currencyService.CreateNewCurrencyAsync(currency);
            return Created("/currencies/create", currency);
        }

        [HttpDelete]
        [Route("/currencies/delete")]
        public async Task<IActionResult> DeleteCurrency([FromBody] Currency currency)
        {
            await _currencyService.DeleteCurrencyAsync(currency);
            return NoContent();
        }
    }
}