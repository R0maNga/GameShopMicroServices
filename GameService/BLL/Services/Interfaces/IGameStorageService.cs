using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.GameStorageInput;
using BLL.Models.GameStorageOutput;

namespace BLL.Services.Interfaces
{
    public interface IGameStorageService
    {
        public Task Create(CreateGameStorageInput gameStorage, CancellationToken toke);
        public Task Update(UpdateGameStorageInput gameStorage, CancellationToken toke);
        public Task Delete(DeleteGameStorageInput gameStorage, CancellationToken toke);
        public Task<GetGameStorageOutput> GetGameStorageById(int id, CancellationToken toke);
    }
}
