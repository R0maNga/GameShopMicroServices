using AutoMapper;
using BLL.Models.Input.OrderInput;
using BLL.Models.Output;
using OrderService.Models.Request.OrderRequest;
using OrderService.Models.Response.OrderResponse;

namespace OrderService.Mapper
{
    public class OrderProfile
    {
        public class MappingProfile:Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateOrderRequest, CreateOrderInput>();
                CreateMap<UpdateOrderRequest, UpdateOrderInput>();
                CreateMap<GetOrderOutput, GetOrderResponse>();
            }
            
        }
    }
}
