using System;

namespace FinancesAPI.Models.Exceptions
{
    public class UnableToFindCurrencyInWalletException : Exception
    {
        public UnableToFindCurrencyInWalletException(string message) : base(message)
        {
        }
    }
}