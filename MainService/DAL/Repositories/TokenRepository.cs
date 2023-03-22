using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts.Repositories;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TokenRepository: AbstractRepository<UserRefreshToken>, ITokenRepository
    {
        private readonly DbSet<UserRefreshToken> _dbSet;

        
        public TokenRepository(DbSet<UserRefreshToken> dbSet) : base(dbSet)
        {

        }

        /*public  void Create(UserRefreshToken item)
        {
            var userRefreshToken = new UserRefreshToken
            {
                CreatedDate = DateTime.Now,
                ExpirationDate = DateTime.UtcNow.AddMinutes(3),
                IpAdress = item.IpAdress,
                IsInvalidated = false,
                RefreshToken = item.RefreshToken,
                Token = item.Token,
                Id = item.Id
            };
                _dbSet.Add(userRefreshToken);
        }

        public void Update(UserRefreshToken token)
        {
            _dbSet.Update(token);
        }*/
    }
}
