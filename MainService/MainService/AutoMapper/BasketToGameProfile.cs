using AutoMapper;
using BLL.Models.Input.BasketToGameInput;
using BLL.Models.Output.BasketToGameOutput;
using MainService.Models.Request.BasketToGameRequest;
using MainService.Models.Response.BasketToGameResponse;

namespace MainService.AutoMapper
{
    public class BasketToGameProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateBasketToGameRequest, CreateGameToBasketInput>();
                CreateMap<UpdateBasketToGameRequest, UpdateGameToBasketInput>();
                CreateMap<DeleteBasketToGameRequest, DeleteGameToBasketInput>();
                CreateMap<GetBasketToGameOutput, BasketToGameResponse>();
                CreateMap<GetBasketToGameOutput, UpdateGameToBasketInput>();
            }
        }
    }
}
