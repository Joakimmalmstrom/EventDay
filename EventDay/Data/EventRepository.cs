using EventDayWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventDayWeb.Data
{
    public class EventRepository
    {
        private readonly EventDayContext db;

        public EventRepository(EventDayContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<EventDay>> GetAllEventsAsync(bool includeLectures)
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

        public async Task<Lecture[]> GetAllLecturesAsync(string eventName, bool includeSpeakers)
        {
            var query = db.Lectures.AsQueryable();

            query = includeSpeakers ? query.Include(l => l.Speaker) : query;

            query = query.Where(l => l.EventDay.Name == eventName.ToUpper());

            return await query.ToArrayAsync();
        }

        public async Task<EventDay> GetEventAsync(string name, bool includeLectures)
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

        public async Task<Models.Entities.EventDay[]> GetAllEventsByDateAsync(DateTime eventDate, bool includeLectures)
        {
            var query = db.EventDays
                .Include(e => e.Location)
                .AsQueryable();

            if (includeLectures)
            {
                query = query.Include(e => e.Lectures)
                             .ThenInclude(e => e.Speaker);
            }

            return await query.Where(e => e.EventDate.Equals(eventDate)).ToArrayAsync();
        }

        public async Task AddAsync<T>(T added)
        {
            await db.AddAsync(added);
        }

        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }
    }
}
