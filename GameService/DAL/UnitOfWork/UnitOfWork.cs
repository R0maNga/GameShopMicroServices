using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DAL.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly GameStorageContext _dbContext;

        public UnitOfWork(GameStorageContext dbContext)
        {
            _dbContext = dbContext;
        }
        public  Task<int> SaveChanges(CancellationToken token)
        {
            return  _dbContext.SaveChangesAsync(token);
        }
    }
}
