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
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Product>> SearchAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
