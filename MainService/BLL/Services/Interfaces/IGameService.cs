using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Input.GameInput;
using BLL.Models.Output.GameOutput;

namespace BLL.Services.Interfaces
{
    public interface IGameService
    {
        public Task CreateGame(CreateGameInput game, CancellationToken token);
        public Task UpdateGame(UpdateGameInput game, CancellationToken token);
        public Task DeleteGame(DeleteGameInput game, CancellationToken token);
        public Task<List<GetGameOutput>> GetGames(CancellationToken token);
        public Task<GetGameOutput> GetGameById(int id, CancellationToken token);
        public Task<GetGameOutput> GetGameByName(string name, CancellationToken token);
    }
}
