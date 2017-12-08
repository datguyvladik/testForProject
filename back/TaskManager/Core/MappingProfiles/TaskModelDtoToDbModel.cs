using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Core.Models.DataAccess;
using TaskManager.Core.Models.DTO;

namespace TaskManager.Core.MappingProfiles
{
    public class TaskModelDtoToDbModel : Profile
    {
        public TaskModelDtoToDbModel()
        {
            CreateMap<TaskModelDto, TaskDbModel>()
                .ForMember(dbm => dbm.Id, opts => opts.MapFrom(dto => dto.Id))
                .ForMember(dbm => dbm.Title, opts => opts.MapFrom(dto => dto.Title))
                .ForMember(dbm => dbm.Content, opts => opts.MapFrom(dto => dto.Content))
                .ForMember(dbm => dbm.CreatedDate, opts => opts.MapFrom(dto => dto.CreatedDate))
                .ReverseMap()
                .ForMember(dto => dto.Id, opts => opts.MapFrom(dbm => dbm.Id))
                .ForMember(dto => dto.Title, opts => opts.MapFrom(dbm => dbm.Title))
                .ForMember(dto => dto.Content, opts => opts.MapFrom(dbm => dbm.Content))
                .ForMember(dto => dto.CreatedDate, opts => opts.MapFrom(dbm => dbm.CreatedDate));
        }
    }
}
