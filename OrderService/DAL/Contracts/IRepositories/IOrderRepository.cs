using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Contracts.IRepositories
{
    public  interface IOrderRepository
    {
        void Create (Order order);
        void Update (Order order);
        void Delete (Order order);

    }
}
