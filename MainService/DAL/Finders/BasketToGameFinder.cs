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
    public class BasketToGameFinder:IBasketToGameFinder
    {
        private readonly DbSet<BasketToGame> _dbSet;

        public BasketToGameFinder(DbSet<BasketToGame> dbSet)
        {
            _dbSet = dbSet;
        }
        public  Task<BasketToGame> GetById(int id, CancellationToken token)
        {
            var res = AsQueryable();
            return res.FirstOrDefaultAsync(x => x.Id == id, token)!;
        }

        public async Task<List<BasketToGame>> GetAllBasketToGameForCurrentBasket(int id, CancellationToken token)
        {
            var res =  AsQueryable();
            var foundBasketToGames= await res.Where(x => x.BasketId == id).ToListAsync(token);
            return  foundBasketToGames;

        }

        protected IQueryable<BasketToGame> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
