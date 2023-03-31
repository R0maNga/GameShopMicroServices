using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChanges(CancellationToken token);
    }
}
