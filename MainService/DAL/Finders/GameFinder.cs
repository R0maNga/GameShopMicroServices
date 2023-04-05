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
    public class GameFinder:IGameFinder
    {
        private readonly DbSet<Game> _dbSet;

        public GameFinder(DbSet<Game> dbSet)
        {
            _dbSet = dbSet;
        }
        public Task<List<Game>> GetAllGames(CancellationToken token)
        {
            var res = AsQueryable();
            return res.ToListAsync(token);
        }

        public Task<Game> FindGameById(int id, CancellationToken token)
        {
            var res = AsQueryable();
            return res.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, token)!;

        }

        public Task<Game> FindGameByName(string name, CancellationToken token)
        {
            var res = AsQueryable();
            return res.SingleOrDefaultAsync(x => x.Name == name, token)!;
        }

        protected IQueryable<Game> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
