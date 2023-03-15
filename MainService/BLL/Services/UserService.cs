using BLL.Models.Input.UserInput;
using BLL.Models.Output.UserOutput;
using BLL.Services.Interfaces;
using DAL;
using Microsoft.Extensions.Options;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly MainServiceContext _context;
        private ITokenService _jwtUtils;
        

        public UserService(
            MainServiceContext context,
            ITokenService jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            
        }
        

        public void RevokeToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetUser>> GetAll(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<GetUser?> GetById(int id, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
