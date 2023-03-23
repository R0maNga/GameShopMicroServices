using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Contracts.IFinders
{
    public interface IOrderFinder
    {
        public Task<Order> GetOrderById(int id, CancellationToken token);
    }
}
