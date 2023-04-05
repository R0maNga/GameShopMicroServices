using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Input.OrderInput;
using BLL.Models.Output;
using BLL.Services.Interfaces;
using DAL.Contracts.IFinders;
using DAL.Contracts.IRepositories;
using DAL.Contracts.IUnitOfWork;
using DAL.Entities;

namespace BLL.Services
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderFinder _finder;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository repository, IOrderFinder finder, IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _finder = finder;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task Create(CreateOrderInput order, CancellationToken token)
        {
            var mappedOrder = _mapper.Map<Order>(order);
            _repository.Create(mappedOrder);
            await _unitOfWork.SaveChanges(token);
        }

        public async Task Update(UpdateOrderInput order, CancellationToken token)
        {
            var mappedOrder = _mapper.Map<Order>(order);
            _repository.Update(mappedOrder);
            await _unitOfWork.SaveChanges(token);
        }

        public async Task<GetOrderOutput> GetOrderById(int id, CancellationToken token)
        {
            var foundOrder = await _finder.GetOrderById(id, token);
            return _mapper.Map<GetOrderOutput>(foundOrder);
        }

        public async Task ConfirmOrder(int id, CancellationToken token)
        {
            await EditOrderStatus(id, token, "Paid");
        }

        public async Task CancelOrder(int id, CancellationToken token)
        {
            await EditOrderStatus(id, token, "Canceled");
        }

        private async Task EditOrderStatus(int id, CancellationToken token, string orderStatus)
        {
            var foundOrder = await _finder.GetOrderById(id, token);
            var changedOrder = new Order()
            {
                Id = foundOrder.Id,
                BasketId = foundOrder.BasketId,
                Price = foundOrder.Price,
                OrderStatus = orderStatus
            };
            _repository.Update(changedOrder);
            await _unitOfWork.SaveChanges(token);
        }
    }
}
