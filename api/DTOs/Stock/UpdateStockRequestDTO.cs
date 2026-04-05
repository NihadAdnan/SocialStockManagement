using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Stock
{
    public class UpdateStockRequestDTO
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol can not be more then 10 characters!")]
        public string Symbol { get; set; } = "";

        [Required]
        [MaxLength(100, ErrorMessage = "Company Name can not be more then 100 characters!")]
        public string CompanyName { get; set; } = "";

        [Required]
        [Range(1,1000000000)]
        public decimal Purchase { get; set; }

        [Range(0.001,100)]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = "";

        [Required]
        [Range(1,10000000000)]
        public long MarketCap { get; set; }
    }
}