using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Input.GameInput;
using BLL.Models.Output.GameOutput;
using DAL.Entityes;

namespace BLL.AutoMapper
{
    public class GameProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateGameInput, Game>();
                CreateMap<UpdateGameInput, Game>();
                CreateMap<DeleteGameInput, Game>();
                CreateMap<Game, GetGameOutput>();
                CreateMap<GetGameOutput, GameOutputForGameService>()
                    .ForMember(dest => dest.SoldGames, opt => opt.MapFrom(src => 0))
                    .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.GamesInStorage, opt => opt.MapFrom(src => src.Quantity))
                    .ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.Name))
                    .ForSourceMember(source => source.Discription, o => o.DoNotValidate());


            }
        }
    }
}
