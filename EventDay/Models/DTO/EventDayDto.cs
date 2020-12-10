using EventDayWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventDayWeb.Models.DTO
{
    public class EventDayDto
    {
        [StringLength(10)]
        [Required]
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public int Length { get; set; }


        public ICollection<LectureDto> Lectures { get; set; }

        public string LocationAddress { get; set; }
        public string LocationCityTown { get; set; }
        public string LocationStateProvince { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationCountry { get; set; }
    }
}
