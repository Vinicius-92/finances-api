using FinancesAPI.Hateoas;

namespace FinancesAPI.Models.ResponseModels
{
    public class CurrencyContainer
    {
        public CurrencyContainer()
        {
        }

        public CurrencyContainer(Currency currency, Link[] links)
        {
            Currency = currency;
            Links = links;
        }

        public Currency Currency { get; set; }
        public Link[] Links { get; set; }
    }
}