using AutoMapper;
using BLL.Models.Input.BasketToGameInput;
using BLL.Models.Input.GameInput;
using BLL.Models.Output.BasketToGameOutput;
using BLL.Models.Output.GameOutput;
using MainService.Models.Request.BasketToGameRequest;
using MainService.Models.Response.BasketToGameResponse;
using MainService.Models.Response.GameResponse;

namespace MainService.AutoMapper
{
    public class BasketToGameProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateBasketToGameRequest, CreateBasketToGameInput>();
                CreateMap<UpdateBasketToGameRequest, UpdateBasketToGameInput>();
                CreateMap<DeleteBasketToGameRequest, DeleteBasketToGameInput>();

                CreateMap<GetBasketToGameOutput, DeleteBasketToGameInput>();
                
            }
        }
    }
}
