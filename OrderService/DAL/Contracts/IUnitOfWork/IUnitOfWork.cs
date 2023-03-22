using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts.IUnitOfWork
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChanges(CancellationToken token);
    }
}
