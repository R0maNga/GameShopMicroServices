using AutoMapper;
using BLL.Models.Input.BasketInput;
using BLL.Models.Output.BasketOutput;
using MainService.Models.Request.BasketRequest;
using MainService.Models.Response.BasketResponse;


namespace MainService.AutoMapper
{
    public class BasketProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateBasketRequest, CreateBasketInput>();
                CreateMap<UpdateBasketRequest, UpdateBasketInput>();
                CreateMap<GetBasketOutput, GetBasketResponse>();
            }
        }
    }
}
