using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventDay.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventDay.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventDaysController : ControllerBase
    {
        private EventRepository repo;

        public EventDaysController(EventDayContext context)
        {
            repo = new EventRepository(context);
        }
    }
}
