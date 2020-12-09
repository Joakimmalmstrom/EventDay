using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventDay.Models.Entities
{
    public class EventDay
    {
        public int Id { get; set; }

        private string name;
        public string Name { get => name; set => name = value.ToUpper(); }
        public DateTime EventDate { get; set; }
        public int Length { get; set; }
        public int LocationId { get; set; }


        public Location Location { get; set; }
        public ICollection<Lecture> Lectures { get; set; }

    }
}
