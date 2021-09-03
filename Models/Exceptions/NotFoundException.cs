using System;

namespace FinancesAPI.Models.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
        {
        }
    }
}