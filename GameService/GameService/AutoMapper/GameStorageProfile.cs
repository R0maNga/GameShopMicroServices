using AutoMapper;
using BLL.Models.GameStorageInput;
using BLL.Models.GameStorageOutput;
using GameService.Models.GameStorageRequest;
using GameService.Models.GameStorageResponse;

namespace GameService.AutoMapper
{
    public class GameStorageProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateGameStorageRequest, CreateGameStorageInput>();
                CreateMap<UpdateGameStorageRequest, UpdateGameStorageInput>();
                CreateMap<DeleteGameStorageRequest, DeleteGameStorageInput>();
                CreateMap<GetGameStorageOutput, UpdateGameStorageInput>();
                CreateMap<GetGameStorageOutput, GetGameStorageResponse>();
            }
        }
    }
}
