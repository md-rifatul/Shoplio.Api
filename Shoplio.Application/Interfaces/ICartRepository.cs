using Shoplio.Application.Interfaces.IRepository;
using Shoplio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoplio.Application.Interfaces
{
    public interface ICartRepository : IReadRepository<Cart>, IWriteRepository<Cart>
    {
        Task<IEnumerable<Cart>> SearchAsync(string searchTerm);
    }
}
