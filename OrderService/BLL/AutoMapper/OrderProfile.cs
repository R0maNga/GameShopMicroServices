using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Input.OrderInput;
using BLL.Models.Output;
using DAL.Entities;

namespace BLL.AutoMapper
{
    public class OrderProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateOrderInput, Order>();
                CreateMap<UpdateOrderInput, Order>();
                CreateMap<Order,GetOrderOutput>();
            }
        }
    }
}
