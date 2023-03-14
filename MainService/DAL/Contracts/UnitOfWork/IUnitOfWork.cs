using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChanges(CancellationToken token);
    }
}
