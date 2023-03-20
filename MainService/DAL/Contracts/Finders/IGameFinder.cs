using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entityes;

namespace DAL.Contracts.Finders
{
    public interface IGameFinder
    {
        public Task<List<Game>> GetAllGames(CancellationToken token);
        public Task<Game> FindGameById(int id, CancellationToken token);
        public Task<Game> FindGameByName(string name, CancellationToken token);
    }
}
