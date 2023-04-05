using AutoMapper;
using BLL.Models.Input.UserInput;
using BLL.Models.Output.UserOutput;
using BLL.Services.Interfaces;
using DAL;
using DAL.Contracts.Repositories;
using DAL.Contracts.UnitOfWork;
using DAL.Entityes;
using Microsoft.Extensions.Options;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        

        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
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

        public async Task CreateAsync(CreateUser model, CancellationToken token)
        {
            var mappedData = _mapper.Map<User>(model);
            _userRepository.Create(mappedData);
            await _unitOfWork.SaveChanges(token);
        }
    }
}
