using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts.Finders;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Finders
{
    public class TokenFinder:ITokenFinder
    {
        private readonly DbSet<UserRefreshToken> _dbSet;
        private readonly MainServiceContext _context;

        public TokenFinder(DbSet<UserRefreshToken> dbSet, MainServiceContext context)
        {
            _dbSet = dbSet;
            _context = context;
        }
        public UserRefreshToken FindToken(string refreshToken, string ipAdress, string token, bool isInvalidated)
        {
           
            var userRefreshToken = _context.UserRefreshTokens.AsNoTracking().FirstOrDefault(
                x => x.IsInvalidated == isInvalidated 
                     && x.Token == token 
                     && x.RefreshToken == refreshToken
                     && x.IpAdress == ipAdress);

            return userRefreshToken;
        }

        protected IQueryable <UserRefreshToken> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
