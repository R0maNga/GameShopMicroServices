using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Input.GameInput;
using BLL.Models.Output.GameOutput;
using BLL.Services.Interfaces;
using DAL.Contracts.Finders;
using DAL.Contracts.Repositories;
using DAL.Contracts.UnitOfWork;
using DAL.Entityes;

namespace BLL.Services
{
    public class GameService:IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameFinder _gameFinder;

        public GameService(IGameRepository gameRepository, IMapper mapper, IUnitOfWork unitOfWork, IGameFinder gameFinder)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _gameFinder = gameFinder;
        }
        public async Task CreateGame(CreateGameInput game, CancellationToken token)
        {
            var mappedGame = _mapper.Map<Game>(game);
            _gameRepository.Create(mappedGame);
            await _unitOfWork.SaveChanges(token);
        }

        public async Task UpdateGame(UpdateGameInput game, CancellationToken token)
        {
            var mappedGame = _mapper.Map<Game>(game);
            _gameRepository.Update(mappedGame);
            await _unitOfWork.SaveChanges(token);
        }

        public async Task DeleteGame(DeleteGameInput game, CancellationToken token)
        {
            var mappedGame = _mapper.Map<Game>(game);
            _gameRepository.Delete(mappedGame);
            await _unitOfWork.SaveChanges(token);
        }

        public async Task<List<GetGameOutput>> GetGames(CancellationToken token)
        {
           var data = await _gameFinder.GetAllGames(token);
           var mappedGames = _mapper.Map<List<GetGameOutput>>(data);

           return mappedGames;
        }

        public async Task<GetGameOutput> GetGameById(int id, CancellationToken token)
        {
            var data = await _gameFinder.FindGameById(id, token);
            var mappedGame = _mapper.Map<GetGameOutput>(data);

            return mappedGame;
        }

        public async Task<GetGameOutput> GetGameByName(string name, CancellationToken token)
        {
            var data = await _gameFinder.FindGameByName(name, token);
            var mappedGame = _mapper.Map<GetGameOutput>(data);

            return mappedGame;
        }
    }
}
