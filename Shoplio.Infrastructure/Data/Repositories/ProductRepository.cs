using Microsoft.EntityFrameworkCore;
using Shoplio.Application.Interfaces;
using Shoplio.Domain.Entities;
using Shoplio.Infrastructure.Data.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoplio.Infrastructure.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .Include(p=>p.Category)
                .Include(p=>p.Seller)
                .ToListAsync();
            return products;
        }
    }
}
