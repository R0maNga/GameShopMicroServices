using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Input.BasketInput;
using BLL.Models.Input.BasketToGameInput;
using BLL.Models.Output.BasketOutput;
using BLL.Models.Output.GameOutput;
using BLL.Services.Interfaces;
using DAL.Contracts.Finders;
using DAL.Contracts.Repositories;
using DAL.Contracts.UnitOfWork;
using DAL.Entityes;
using DAL.Finders;
using DAL.Repositories;

namespace BLL.Services
{
    public class BasketService:IBasketService
    {
        private readonly IBasketRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBasketFinder _basketFinder;


        public BasketService(IBasketRepository repository, IUnitOfWork unitOfWork, IMapper mapper, 
            IBasketFinder basketFinder)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _basketFinder = basketFinder;
        }
        public async Task CreateBasket(CreateBasketInput basket, CancellationToken token)
        {
            var mappedGame = _mapper.Map<Basket>(basket);
            _repository.Create(mappedGame);
            await _unitOfWork.SaveChanges(token);
        }

        public async Task UpdateBasket(UpdateBasketInput basket, CancellationToken token)
        {
            var mappedData = _mapper.Map<Basket>(basket);
            _repository.Update(mappedData);
            await _unitOfWork.SaveChanges(token);
        }

        public async Task<GetBasketOutput> GetBasketById(int id, CancellationToken token)
        {
            var data = await _basketFinder.GetById(id, token);
            var mappedGame = _mapper.Map<GetBasketOutput>(data);

            return mappedGame;
        }
    }
}
