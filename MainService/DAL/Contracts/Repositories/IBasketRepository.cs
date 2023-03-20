using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entityes;

namespace DAL.Contracts.Repositories
{
    public interface IBasketRepository
    {
        void Create(Basket basket);
        void Update(Basket basket);
        void Delete(Basket basket);
    }
}
