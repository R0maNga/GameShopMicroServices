using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entityes;

namespace DAL.Contracts.Finders
{
    public interface IBasketToGameFinder
    {
        Task<BasketToGame> GetById(int id, CancellationToken token);
        Task<List<BasketToGame>> GetAllBasketToGameForCurrentBasket(int id, CancellationToken token);
    }
}
