using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.GameStorageInput;
using BLL.Models.GameStorageOutput;
using DAL.Entityes;

namespace BLL.AutoMapper
{
    public class GameStorageProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<CreateGameStorageInput, GameStorage>();
                CreateMap<UpdateGameStorageInput, GameStorage>();
                CreateMap<DeleteGameStorageInput, GameStorage>();
                CreateMap<GameStorage, GetGameStorageOutput>();
                CreateMap<GetGameStorageOutput, GameStorage>();

            }
        }
    }
}
