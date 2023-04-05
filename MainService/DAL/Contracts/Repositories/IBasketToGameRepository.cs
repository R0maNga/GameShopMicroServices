using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entityes;

namespace DAL.Contracts.Repositories
{
    public interface IBasketToGameRepository
    {
        void Create(BasketToGame basketToGameRepository);
        void Update(BasketToGame basketToGameRepository);
        void Delete(BasketToGame basketToGameRepository);
    }
}
