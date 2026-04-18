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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Category>> SearchAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
