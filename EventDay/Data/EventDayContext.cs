using EventDayWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventDayWeb.Data
{
    public class EventDayContext : DbContext
    {
        public DbSet<EventDayWeb.Models.Entities.EventDay> EventDays { get; set; }
        public DbSet<Lecture> Lectures { get; set; }

        public EventDayContext(DbContextOptions<EventDayContext> options): base(options){ }
    }
}
