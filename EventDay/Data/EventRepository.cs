using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventDay.Data
{
    public class EventRepository
    {
        private readonly EventDayContext db;

        public EventRepository(EventDayContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<EventDay.Models.Entities.EventDay>> GetAllEvents()
        {
            return await db.EventDays
                .Include(e => e.Location)
                .ToListAsync();
        }
    }
}
