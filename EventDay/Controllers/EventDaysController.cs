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
            var result = await repo.GetAllEvents();
            var mappedResult = mapper.Map<IEnumerable<EventDayDto>>(result);
            return Ok(mappedResult);
        }
    }
}
