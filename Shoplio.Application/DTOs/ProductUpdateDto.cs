using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoplio.Application.DTOs
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? SKU { get; set; }
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        public int SellerId { get; set; }
        public ICollection<string>? ImageUrls { get; set; }
    }
}
