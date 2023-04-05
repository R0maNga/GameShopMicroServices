using AutoMapper;
using BLL.Models.Input.OrderInput;
using BLL.Services.Interfaces;
using DAL.Contracts.IRepositories;
using DAL.Contracts.IUnitOfWork;
using DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts.IFinders;

namespace BLL.Services
{
    public class OrderHostedService : IOrderHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        public OrderHostedService(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message, CancellationToken token)
        {
            AddPlatform(message, token);
        }

        public void ValidateOrder(string message, CancellationToken token)
        {
            CheckOrderStatus(message,token);
        }

        private async void AddPlatform(string platformPublishedMessage, CancellationToken token)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                var unit = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var platformPublishedDto = JsonConvert.DeserializeObject<CreateOrderInput>(platformPublishedMessage);

                try
                {
                    
                    var  mappedOrder = _mapper.Map<Order>(platformPublishedDto);
                    repo.Create(mappedOrder);
                    await unit.SaveChanges(token);
                    
                   
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Coud not platform to DB{e.Message}");
                    throw;
                }
            }
        }

        private async void CheckOrderStatus(string platformPublishedMessage, CancellationToken token)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                var unit = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var finder = scope.ServiceProvider.GetRequiredService<IOrderFinder>();

                var platformPublishedDto = JsonConvert.DeserializeObject<CreateOrderInput>(platformPublishedMessage);

                try
                {

                    var mappedOrder = _mapper.Map<Order>(platformPublishedDto);
                    var foundOrder = await finder.GetLastWaitingOrder(token);
                    foundOrder.OrderStatus = mappedOrder.OrderStatus;
                    repo.Update(foundOrder);
                    await unit.SaveChanges(token);


                }
                catch (Exception e)
                {
                    Console.WriteLine($"Coud not platform to DB{e.Message}");
                    throw;
                }
            }
        }
    }
}

