using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Input.BasketInput;
using BLL.Models.Output.BasketOutput;
using BLL.Models.Output.BasketToGameOutput;
using DAL.Entityes;

namespace BLL.AutoMapper
{
    public class BasketProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateBasketInput, Basket>();
                CreateMap<UpdateBasketInput, Basket>();
                CreateMap<Basket, GetBasketOutput>();
            }
        }
    }
}
