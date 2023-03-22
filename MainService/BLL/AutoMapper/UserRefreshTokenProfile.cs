using AutoMapper;
using BLL.Models.Input.UserInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Output.RefreshTokenOutput;
using DAL.Entityes;

namespace BLL.AutoMapper
{
    public class UserRefreshTokenProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<UserRefreshTokenOutput, UserRefreshToken>();
                CreateMap<UserRefreshToken, UserRefreshTokenOutput>();
            }
        }
    }
}
