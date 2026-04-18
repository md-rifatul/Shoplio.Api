using Shoplio.Application.Interfaces.IRepository;
using Shoplio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoplio.Application.Interfaces
{
    public interface IProductRepository : IReadRepository<Product>, IWriteRepository<Product>
    {
        Task<IEnumerable<Product>> SearchAsync(string searchTerm);
    }
}
