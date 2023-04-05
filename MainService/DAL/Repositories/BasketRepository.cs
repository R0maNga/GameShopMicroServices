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
    public class BasketRepository : AbstractRepository<Basket>, IBasketRepository
    {
        public BasketRepository(DbSet<Basket> dbSet) : base(dbSet)
        {
            
        }
    }
}
