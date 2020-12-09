using EventDayWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventDayWeb.Data
{
    public class SeedData
    {
        internal static async Task InitializeAcync(IServiceProvider services)
        {
            using var db = new EventDayContext(services.GetRequiredService<DbContextOptions<EventDayContext>>());

            if (!await db.EventDays.AnyAsync())
            {
                await db.EventDays.AddAsync(new Models.Entities.EventDay()
                {
                    Name = "NET20",
                    EventDate = new DateTime(2020, 12, 18),
                    Location = new Location()
                    {
                        Address = "Storgatan 12",
                        CityTown = "Stockholm",
                        StateProvince = "Stockholm",
                        PostalCode = "12345",
                        Country = "Sweden"
                    },
                    Length = 1,
                    Lectures = new Lecture[]
                        {
                            new Lecture
                            {
                              Title = "Entity Framework From Scratch",
                              Level = 100,
                              Speaker = new Speaker
                              {
                                FirstName = "Kalle",
                                LastName = "Anka",
                                BlogUrl = "http://KalleAnka.com",
                                Company = "´Disney",
                                CompanyUrl = "http://Disney.com",
                                GitHub = "KalleKodar"
                              }
                          },
                            new Lecture
                            {
                              Title = "Writing Sample Data Made Easy",
                              Level = 200,
                              Speaker = new Speaker
                              {
                                FirstName = "Knatte",
                                LastName = "Anka",
                                BlogUrl = "http://Knatte.com",
                                Company = "Slangbellan",
                                CompanyUrl = "http://Disney.com",
                                GitHub = "Knatte",
                              }
                            }
                           }
                });
                await db.SaveChangesAsync();
            }
            }
    }
}
