using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts.IRepositories;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderRepository : AbstractRepository<Order>, IOrderRepository
    {

        public OrderRepository(OrderServiceContext _context, DbSet<Order> _dbSet) : base(_dbSet,_context )
        {
            
        }
    }
}
