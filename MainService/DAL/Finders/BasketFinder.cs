using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts.Finders;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Finders
{
    public class BasketFinder:IBasketFinder
    {
        private readonly DbSet<Basket> _dbSet;

        public BasketFinder(DbSet<Basket> dbSet)
        {
            _dbSet = dbSet;
        }
        public Task<Basket> GetById(int id, CancellationToken token)
        {
            var res = AsQueryable();

            return res.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, token)!;
        }

        protected IQueryable<Basket> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
