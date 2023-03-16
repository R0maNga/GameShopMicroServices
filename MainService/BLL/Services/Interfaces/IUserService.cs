using BLL.Models.Output.UserOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Input.UserInput;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        
        void RevokeToken(string token, string ipAddress);
        Task<List<GetUser>> GetAll(CancellationToken token);
        Task<GetUser?> GetById(int id, CancellationToken token);
        public Task CreateAsync(CreateUser model, CancellationToken token);
    }
}
