using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts.IUnitOfWork;

namespace DAL.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly OrderServiceContext _context;

        public UnitOfWork(OrderServiceContext context)
        {
            _context = context;
        }
        public Task<int> SaveChanges(CancellationToken token)
        {
            return _context.SaveChangesAsync(token);
        }
    }
}
