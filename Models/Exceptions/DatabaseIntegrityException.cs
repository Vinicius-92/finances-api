using System;

namespace FinancesAPI.Models.Exceptions
{
    public class DatabaseIntegrityException : Exception
    {
        public DatabaseIntegrityException(string message) : base(message)
        {
        }
    }
}