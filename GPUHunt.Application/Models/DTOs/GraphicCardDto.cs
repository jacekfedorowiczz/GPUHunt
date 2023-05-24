using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUHunt.Application.Models.DTOs
{
    public class GraphicCardDto
    {
        public string Model { get; set; }
        public string Vendor { get; set; }
        public decimal LowestPrice { get; set; }
        public string LowestPriceShop { get; set; }
        public decimal? HighestPrice { get; set; } = default!;
        public string? HighestPriceShop { get; set; } = default!;
        public bool IsPriceEqual { get; set; }
    }
}
