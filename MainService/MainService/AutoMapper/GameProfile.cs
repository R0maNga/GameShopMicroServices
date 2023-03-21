using AutoMapper;
using BLL.AutoMapper;
using BLL.Models.Input.GameInput;
using BLL.Models.Output.GameOutput;
using MainService.Models.Request.GemeRequest;
using MainService.Models.Response.GameResponse;

namespace MainService.AutoMapper
{
    public class GameProfile
    {
        public class MappingProfile:Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateGameRequest,CreateGameInput>();
                CreateMap<UpdateGameRequest,UpdateGameInput>();
                CreateMap<DeleteGameRequest,DeleteGameInput>();
                CreateMap<GetGameOutput,DeleteGameInput>();
                CreateMap<GetGameOutput, GetGameResponse>();
            }
        }
    }
}
