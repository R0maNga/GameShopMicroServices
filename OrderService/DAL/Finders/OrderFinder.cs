using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts.IFinders;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Finders
{
    public class OrderFinder:IOrderFinder
    {
        private readonly DbSet<Order> _dbSet;

        public OrderFinder(DbSet<Order> dbSet)
        {
            _dbSet = dbSet;
        }
        public Task<Order> GetOrderById(int id, CancellationToken token)
        {
            var result = AsQueryable();
            return result.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, token)!;
        }

        protected IQueryable<Order> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
