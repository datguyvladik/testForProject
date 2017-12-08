using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Core.Models.DataAccess;
using TaskManager.Core.Models.DTO;

namespace TaskManager.Core.MappingProfiles
{
    public class UserDtotoUserDbModel : Profile
    {
        public UserDtotoUserDbModel()
        {
            CreateMap<UserDto, UserDbModel>()
                .ForMember(dbm => dbm.Email, opts => opts.MapFrom(dto => dto.Email))
                .ForMember(dbm => dbm.UserName, opts => opts.MapFrom(dto => dto.Email));
        }
    }
}
