using EventDay.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventDay.Data
{
    public class EventDayContext : DbContext
    {
        public DbSet<EventDay.Models.Entities.EventDay> EventDays { get; set; }
        public DbSet<Lecture> Lectures { get; set; }

        public EventDayContext(DbContextOptions<EventDayContext> options): base(options){ }
    }
}
