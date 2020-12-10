using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventDayWeb.Data;
using EventDayWeb.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventDayWeb.Models.Entities;

namespace EventDayWeb.Controllers
{
    [Route("api/events/{name}/lectures")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private IMapper mapper;
        private readonly EventRepository repo;
        public LecturesController(EventDayContext db, IMapper mapper)
        {
            repo = new EventRepository(db);
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<LectureDto>> GetAll(string name, bool includeSpeakers = false)
        {
            var lectures = await repo.GetAllLecturesAsync(name, includeSpeakers);
            var model = mapper.Map<LectureDto[]>(lectures);
            return Ok(model);
        } 
        
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<LectureDto>> Get(int id, bool includeSpeakers = false)
        {
            
            return Ok();
        }


        [HttpPost]
        //ToDo Validate speakerId
        public async Task<ActionResult<LectureDto>> AddLecture(string name, LectureDto model)
        {
            var eventId = (await repo.GetEventAsync(name, false))?.Id;

            if (eventId is null) return NotFound();

            var lecture = mapper.Map<Lecture>(model);
            lecture.EventDayId = (int)eventId;
            await repo.AddAsync(lecture);

            if (await repo.SaveAsync())
            {
                return CreatedAtAction("Get", new { id = lecture.Id, name }, mapper.Map<LectureDto>(lecture));
            }
            else
                return StatusCode(500);

        }

    }
}
