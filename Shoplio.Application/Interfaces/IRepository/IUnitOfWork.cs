using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoplio.Application.Interfaces.IRepository
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
