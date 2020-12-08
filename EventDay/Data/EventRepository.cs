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
    }
}
