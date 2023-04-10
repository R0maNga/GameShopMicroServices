using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts.IFinder;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Finders
{
    public class GameStorageFinder:IGameStorageFinder
    {
        private readonly DbSet<GameStorage> _dbSet;

        public GameStorageFinder(DbSet<GameStorage> dbSet)
        {
            _dbSet = dbSet;
        }
        public Task<GameStorage> FindGameStorageById(int id, CancellationToken token)
        {
            var data = AsQueryable();

            return data.AsNoTracking().SingleOrDefaultAsync(x=>x.Id==id, token)!;
        }

        public Task<GameStorage> FindGameStorageByGameId(int id, CancellationToken token)
        {
            var data = AsQueryable();

            return data.AsNoTracking().SingleOrDefaultAsync(x => x.GameId == id, token)!;
        }

        protected  IQueryable<GameStorage> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
