using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Input.BasketToGameInput;
using BLL.Models.Output.BasketToGameOutput;
using DAL.Entityes;

namespace BLL.AutoMapper
{
    public class BasketToGameProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateGameToBasketInput, BasketToGame>();
                CreateMap<UpdateGameToBasketInput, BasketToGame>();
                CreateMap<DeleteGameToBasketInput, BasketToGame>();
                CreateMap<BasketToGame, GetBasketToGameOutput>();
            }
        }
    }
}
