using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Input.BasketToGameInput;
using BLL.Models.Output.BasketToGameOutput;
using BLL.Services.Interfaces;
using DAL.Contracts.Finders;
using DAL.Contracts.Repositories;
using DAL.Contracts.UnitOfWork;
using DAL.Entityes;

namespace BLL.Services
{
    public class BasketToGameService:IBasketToGameService
    {
        private readonly IBasketToGameRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBasketToGameFinder _finder;
        private readonly IMessageProducer _producer;
        public BasketToGameService(IBasketToGameRepository repository, IUnitOfWork unitOfWork, IMapper mapper, 
            IBasketToGameFinder finder, IMessageProducer producer)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _finder = finder;
            _producer = producer;
        }
        public async Task CreateBasketToGame(CreateBasketToGameInput gameToBasket, CancellationToken token)
        {
            var mappedData = _mapper.Map<BasketToGame>(gameToBasket);
            _repository.Create(mappedData);
            await _unitOfWork.SaveChanges(token);
        }

        public async Task UpdateBasketToGame(UpdateBasketToGameInput gameToBasket, CancellationToken token)
        {
            var mappedData = _mapper.Map<BasketToGame>(gameToBasket);
            _repository.Update(mappedData);
            await _unitOfWork.SaveChanges(token);
        }

        public async Task DeleteBasketToGame(DeleteBasketToGameInput gameToBasket, CancellationToken token)
        {
            var mappedData = _mapper.Map<BasketToGame>(gameToBasket);
            _repository.Delete(mappedData);
            await _unitOfWork.SaveChanges(token);
        }

        public async Task<GetBasketToGameOutput> GetBasketToGameById(int id, CancellationToken token)
        {
            var foundData = await _finder.GetById(id, token);

            return _mapper.Map<GetBasketToGameOutput>(foundData);
        }

        public async Task<List<GetBasketToGameOutput>> GetAllBasketToGameByBasketId(int id, CancellationToken token, bool includeGame = false)
        {
            var foundData = await _finder.GetAllBasketToGameForCurrentBasket(id, token, includeGame);

            return _mapper.Map<List<GetBasketToGameOutput>>(foundData);
        }

        public void SendGamesToStorage(List<GetBasketToGameOutput> basketToGame)
        {
            var gamesToStorageOutputs = new List<GamesToStorageOutput> ();
            foreach (var games in basketToGame)
            {
                var item = new GamesToStorageOutput();
                item.Name = games.Game.Name;
                item.Id = games.GameId;
                item.SoldGames = games.GameAmount;
                item.BasketId = games.BasketId;
                gamesToStorageOutputs.Add(item);

            }
            _producer.SendMessage(gamesToStorageOutputs, "GameStorage");
        }

        public decimal CalculateTotalPrice(List<GetBasketToGameOutput> basketToGame)
        {
            decimal total = 0;
            foreach (var item in basketToGame)
            {
                total += item.Game.Priсe;
            }

            return total;
        }
    }
}
