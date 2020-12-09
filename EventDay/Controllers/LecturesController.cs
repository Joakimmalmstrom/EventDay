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

    }
}
