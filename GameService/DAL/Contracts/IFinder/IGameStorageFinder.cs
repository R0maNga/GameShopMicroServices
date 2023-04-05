using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entityes;

namespace DAL.Contracts.IFinder
{
    public interface IGameStorageFinder
    {
        public Task<GameStorage> FindGameStorageById(int id, CancellationToken token);
    }
}
