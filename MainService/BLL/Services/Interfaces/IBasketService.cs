using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Input.BasketInput;
using BLL.Models.Input.BasketToGameInput;
using BLL.Models.Output.BasketOutput;
using BLL.Models.Output.GameOutput;

namespace BLL.Services.Interfaces
{
    public interface IBasketService
    {
        public Task CreateBasket(CreateBasketInput basket, CancellationToken token);
        public Task UpdateBasket(UpdateBasketInput gameToBasket, CancellationToken token);
        public Task<GetBasketOutput> GetBasketById(int id, CancellationToken token);
    }
}
