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
            }
        }
    }
}
