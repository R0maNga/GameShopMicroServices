using AutoMapper;
using BLL.Models.Input.UserInput;
using BLL.Models.Output.RefreshTokenOutput;
using DAL.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AutoMapper
{
    public class UserProfile
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<User, CreateUser>();
                CreateMap<CreateUser, User>();
            }
        }
    }
}
