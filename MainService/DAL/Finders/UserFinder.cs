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
    public class UserFinder:IUserFinder
    {
        private readonly DbSet<User> _dbSet;
        public UserFinder(DbSet<User> dbSet)
        {
            _dbSet = dbSet;
        }
        public Task<List<User>> GetAsync(CancellationToken token)
        {
            var res = AsQueryable();

            return res.ToListAsync(token);
        }

        public Task<User> GetByIdAsync(int id, CancellationToken token)
        {
            var res = AsQueryable();
            return res.FirstOrDefaultAsync(x => x.Id == id, token)!;
        }

        public User? GetUserByNameAndPassword(string password, string userName, CancellationToken token)
        {
            var res = AsQueryable();
            var user = res.FirstOrDefault(x => x.UserName.Equals(userName)
                                                                && x.Password.Equals(password));
            
            return user;
        }

        protected IQueryable<User> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
