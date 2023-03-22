﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Input.BasketToGameInput;
using BLL.Models.Output.BasketToGameOutput;
using BLL.Models.Output.GameOutput;

namespace BLL.Services.Interfaces
{
    public interface IBasketToGameService
    {
        public Task CreateBasketToGame(CreateBasketToGameInput gameToBasket, CancellationToken token);
        public Task UpdateBasketToGame(UpdateBasketToGameInput gameToBasket, CancellationToken token);
        public Task DeleteBasketToGame(DeleteBasketToGameInput gameToBasket, CancellationToken token);
        public Task<GetBasketToGameOutput> GetBasketToGameById(int id, CancellationToken token);
        public Task<List<GetBasketToGameOutput>> GetAllBasketToGameByBasketId(int id, CancellationToken token);
    }
}
