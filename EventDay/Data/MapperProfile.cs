using AutoMapper;
using EventDay.Models.DTO;
using EventDay.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventDay.Data
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
