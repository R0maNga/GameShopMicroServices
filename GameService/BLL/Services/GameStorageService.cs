using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.GameStorageInput;
using BLL.Models.GameStorageOutput;
using BLL.Services.Interfaces;
using DAL.Contracts.IFinder;
using DAL.Contracts.Repositories;
using DAL.Contracts.UnitOfWork;
using DAL.Entityes;

namespace BLL.Services
{
    public class GameStorageService:IGameStorageService
    {
        private readonly IGameStorageRepository _repository;
        private readonly IGameStorageFinder _finder;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public GameStorageService(IGameStorageRepository repository, IGameStorageFinder finder, IMapper mapper, 
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _finder = finder;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task Create(CreateGameStorageInput gameStorage, CancellationToken toke)
        {
            var mappedData = _mapper.Map<GameStorage>(gameStorage);
            _repository.Create(mappedData);
            await _unitOfWork.SaveChanges(toke);
        }

        public async Task Update(UpdateGameStorageInput gameStorage, CancellationToken toke)
        {
            var mappedData = _mapper.Map<GameStorage>(gameStorage);
            _repository.Update(mappedData);
            await _unitOfWork.SaveChanges(toke);
        }

        public async Task Delete(DeleteGameStorageInput gameStorage, CancellationToken toke)
        {
            var mappedData = _mapper.Map<GameStorage>(gameStorage);
            _repository.Delete(mappedData);
            await _unitOfWork.SaveChanges(toke);
        }

        public async Task<GetGameStorageOutput> GetGameStorageById(int id, CancellationToken toke)
        {
            var foundGameStorage = await _finder.FindGameStorageById(id, toke);
            var mappedData = _mapper.Map<GetGameStorageOutput>(foundGameStorage);
            return mappedData;
        }
    }
}
