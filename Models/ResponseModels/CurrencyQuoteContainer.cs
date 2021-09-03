using FinancesAPI.Hateoas;

namespace FinancesAPI.Models.ResponseModels
{
    public class CurrencyQuoteContainer
    {
        public CurrencyQuoteContainer()
        {
        }

        public CurrencyQuoteContainer(CurrencyQuote currencyQuote, Link[] links)
        {
            CurrencyQuote = currencyQuote;
            Links = links;
        }

        public CurrencyQuote CurrencyQuote { get; set; }
        public Link[] Links { get; set; }
    }
}