using AutoMapper;
using EventDayWeb.Models.DTO;
using EventDayWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventDayWeb.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Models.Entities.EventDay, EventDayDto>().ReverseMap();
            CreateMap<Lecture, LectureDto>().ReverseMap();
            CreateMap<Speaker, SpeakerDto>().ReverseMap();
        }
    }
}
