using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Input.BasketToGameInput;
using BLL.Models.Output.BasketToGameOutput;
using DAL.Entityes;
using Microsoft.Extensions.Options;

namespace BLL.AutoMapper
{
    public class BasketToGameProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateBasketToGameInput, BasketToGame>();
                CreateMap<UpdateBasketToGameInput, BasketToGame>();
                CreateMap<DeleteBasketToGameInput, BasketToGame>();
                CreateMap<BasketToGame, GetBasketToGameOutput>();

                CreateMap<Game, GameForBusketToGameOutput>();

            }
        }
    }
}
