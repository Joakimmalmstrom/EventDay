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
        public async Task<ActionResult<IEnumerable<EventDayDto>>> GetAllEvents()
        {
           
                var result = await repo.GetAllEventsAsync();
                var mappedResult = mapper.Map<IEnumerable<EventDayDto>>(result);
                return Ok(mappedResult);
           
        } 
        
        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<EventDayDto>> GetEvent(string name)
        {
           
                var result = await repo.GetEventAsync(name);

                if (result is null) return NotFound();

                var mappedResult = mapper.Map<EventDayDto>(result);
                return Ok(mappedResult);
           
        }
    }
}
