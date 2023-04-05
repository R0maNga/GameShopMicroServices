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
    public class BasketToGameRepository : AbstractRepository<BasketToGame>, IBasketToGameRepository
    {
        public BasketToGameRepository(DbSet<BasketToGame> dbSet): base(dbSet)
        {

        }
    }
}
