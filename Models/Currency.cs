using System.ComponentModel.DataAnnotations;

namespace FinancesAPI.Models
{
    public class Currency
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
    }
}