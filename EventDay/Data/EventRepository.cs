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

        public async Task<IEnumerable<EventDay.Models.Entities.EventDay>> GetAllEventsAsync(bool includeLectures)
        {
            return includeLectures ? await db.EventDays
                                              .Include(e => e.Location)
                                              .Include(e => e.Lectures)
                                              .ThenInclude(e => e.Speaker)
                                              .ToListAsync() :
                                     await db.EventDays
                                              .Include(e => e.Location)
                                              .ToListAsync();
        }

        public async Task<Models.Entities.EventDay> GetEventAsync(string name, bool includeLectures)
        {
            //IQueryable<Models.Entities.EventDay> query =  db.EventDays
            //    .Include(e => e.Location);           
            var query = db.EventDays
                .Include(e => e.Location)
                .AsQueryable();

            if (includeLectures)
            {
                query = query.Include(e => e.Lectures)
                             .ThenInclude(e => e.Speaker);
            }

            return await query.FirstOrDefaultAsync(e => e.Name == name);
        }
    }
}
