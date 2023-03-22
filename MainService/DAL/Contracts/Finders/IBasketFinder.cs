using DAL.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts.Finders
{
    public interface IBasketFinder
    {
        Task<Basket> GetById(int id, CancellationToken token);
    }
}
