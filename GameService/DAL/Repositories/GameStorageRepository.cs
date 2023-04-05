using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts.Repositories;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class GameStorageRepository: AbstractRepository<GameStorage> , IGameStorageRepository
    {
        public GameStorageRepository(DbSet<GameStorage> dbSet) : base(dbSet)
        {

        }
    }
}
