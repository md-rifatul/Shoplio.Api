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
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Cart>> SearchAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
