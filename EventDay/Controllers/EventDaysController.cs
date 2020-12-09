using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventDay.Data;
using EventDay.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventDay.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventDaysController : ControllerBase
    {
        private readonly IMapper mapper;
        private EventRepository repo;

        public EventDaysController(EventDayContext context, IMapper mapper)
        {
            repo = new EventRepository(context);
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDayDto>>> GetAllEvents(bool includeLectures = false)
        {

            var result = await repo.GetAllEventsAsync(includeLectures);
            var mappedResult = mapper.Map<IEnumerable<EventDayDto>>(result);
            return Ok(mappedResult);

        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<EventDayDto>> GetEvent(string name, bool includeLectures = false)
        {

            var result = await repo.GetEventAsync(name, includeLectures);

            if (result is null) return NotFound();

            var mappedResult = mapper.Map<EventDayDto>(result);
            return Ok(mappedResult);

        }


        [HttpGet]
        [Route("searchByDate/{eventDate:DateTime}")]
        public async Task<ActionResult<IEnumerable<EventDayDto>>> SearchByEventName(DateTime eventDate, bool includeLectures = false)
        {
            var searchResult = await repo.GetAllEventsByDateAsync(eventDate, includeLectures);
            return Ok(mapper.Map<EventDayDto[]>(searchResult));
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<EventDayDto>>> CreateEvent(EventDayDto dto)
        {
            if (await repo.GetEventAsync(dto.Name, false) != null)
            {
                ModelState.AddModelError("Name", "Name in use");
                return BadRequest(ModelState);
            }

            var eventday = mapper.Map<Models.Entities.EventDay>(dto);
            await repo.AddAsync(eventday);

            if (await repo.SaveAsync())
            {
                var model = mapper.Map<EventDayDto>(eventday);
                return CreatedAtAction(nameof(GetEvent), new { name = model.Name }, model);
            }
            else
            {
                return BadRequest();
            }
        } 
        
        
        [HttpPut("{name}")]

        public async Task<ActionResult<EventDayDto>> PutEvent(string name, EventDayDto dto)
        {
            //Eller Required attribute
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Name required");
            }

            var eventday = await repo.GetEventAsync(name, false);

            if (eventday is null) return StatusCode(StatusCodes.Status404NotFound);

            mapper.Map(dto, eventday);
            if(await repo.SaveAsync())
            {
                return Ok(mapper.Map<EventDayDto>(eventday));
            }
            else
            {
                return StatusCode(500);
            }
           
        }
    }
}
