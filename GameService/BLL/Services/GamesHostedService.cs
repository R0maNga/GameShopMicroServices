using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.GameStorageInput;
using BLL.Services.Interfaces;
using DAL.Contracts.IFinder;
using DAL.Contracts.Repositories;
using DAL.Contracts.UnitOfWork;
using DAL.Entityes;
using DAL.Finders;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BLL.Services
{
    public class GamesHostedService : IGamesHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly IMessageProducer _messageProducer;

        public GamesHostedService(IServiceScopeFactory scopeFactory, IMapper mapper, IMessageProducer messageProducer)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _messageProducer = messageProducer;
        }
        public void AddGameEvent(string message, CancellationToken token)
        {
            AddGame(message,token);
        }

        public void CheckGameStorageEvent(string message, CancellationToken token)
        {
            CheckGameStorage(message,token);
        }

        private async void AddGame(string platformPublishedMessage, CancellationToken token)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IGameStorageRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var platformPublishedDto = JsonConvert.DeserializeObject<CreateGameStorageInput>(platformPublishedMessage);

                try
                {
                    var mappedOrder = _mapper.Map<GameStorage>(platformPublishedDto);
                    repository.Create(mappedOrder);
                    await unitOfWork.SaveChanges(token);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                    throw;
                }
            }
        }

        private async void CheckGameStorage(string platformPublishedMessage, CancellationToken token)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IGameStorageRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var finder = scope.ServiceProvider.GetRequiredService<IGameStorageFinder>();
                var platformPublishedDto = JsonConvert.DeserializeObject <List<UpdateGameStorageFromMainService>>(platformPublishedMessage);
                var validateMessage = new ValidateOrderInput();
                var basketId = 0;
                try
                {
                    foreach (var games in platformPublishedDto)
                    {
                        
                        var foundedGame = await finder.FindGameStorageByGameId(games.Id, token);

                        if (foundedGame.GamesInStorage >= games.SoldGames)
                        {

                            basketId = games.BasketId;
                            var mappedOrder = _mapper.Map<GameStorage>(foundedGame);
                            mappedOrder.GamesInStorage = (mappedOrder.GamesInStorage - games.SoldGames);
                            mappedOrder.SoldGames += games.SoldGames;
                            repository.Update(mappedOrder);

                        }
                        else
                        {
                            validateMessage.BasketId = games.BasketId;
                            validateMessage.OrderStatus = "Canceled";
                            _messageProducer.SendMessage(validateMessage,"ConfirmOrder");
                            return;
                        }
                    }

                    await unitOfWork.SaveChanges(token);
                    validateMessage.BasketId = basketId;
                    validateMessage.OrderStatus = "Confirmed";
                    _messageProducer.SendMessage(validateMessage, "ConfirmOrder");

                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                    throw;
                }
            }
        }
    }
}
