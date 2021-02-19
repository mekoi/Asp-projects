using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Irisi_M_301072868.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext _context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();

        _context.Database.Migrate();

            if (!_context.Clubs.Any())
            {
                _context.Clubs.AddRange(
                    new Club { ClubName = "Girls U6", AgeRange = "4-5 years old", CreatedDate = new DateTime(2002, 10, 22) },
                    new Club { ClubName = "Girls U8", AgeRange = "6-7 years old", CreatedDate = new DateTime(2005, 08, 11) },
                    new Club { ClubName = "Boys U12", AgeRange = "10-11 years old", CreatedDate = new DateTime(2014, 05, 06) },
                    new Club { ClubName = "Boys U16", AgeRange = "14-15 years old", CreatedDate = new DateTime(2015, 09, 10) }
                );

                _context.SaveChanges();
            }

            var girlsU6 = _context.Clubs.Where(c => c.ClubName.Equals("Girls U6")).FirstOrDefault();
            var girlsU8 = _context.Clubs.Where(c => c.ClubName.Equals("Girls U8")).FirstOrDefault();
            var boysU12 = _context.Clubs.Where(c => c.ClubName.Equals("Boys U12")).FirstOrDefault();
            var boysU16 = _context.Clubs.Where(c => c.ClubName.Equals("Boys U16")).FirstOrDefault();

            if (!_context.Players.Any())
            {
                _context.Players.AddRange(
                new Player { FirstName = "Iris", LastName = "Meko", DayOfBirth = new DateTime(2015, 06, 05), Gender = "Female", PhoneNo = "123-456-7890", Email = "irismeko@hotmail.com", ClubId = Convert.ToInt32(girlsU6.ClubId), ClubJoinedDate = new DateTime(2019, 12, 05) },
                new Player { FirstName = "Albana", LastName = "Elmazi", DayOfBirth = new DateTime(2015, 09, 11), Gender = "Female", PhoneNo = "987-456-7890", Email = "albanaelmazi@hotmail.com", ClubId = Convert.ToInt32(girlsU8.ClubId), ClubJoinedDate = new DateTime(2019, 12, 05) },
                new Player { FirstName = "Etna", LastName = "Meko", DayOfBirth = new DateTime(2013, 01, 13), Gender = "Female", PhoneNo = "987-456-8791", Email = "etnameko@hotmail.com", ClubId = Convert.ToInt32(girlsU8.ClubId), ClubJoinedDate = new DateTime(2019, 12, 05) },
                new Player { FirstName = "Arb", LastName = "Elmazi", DayOfBirth = new DateTime(2008, 01, 25), Gender = "Male", PhoneNo = "874-456-8791", Email = "arbelmazi@hotmail.com", ClubId = Convert.ToInt32(boysU12.ClubId), ClubJoinedDate = new DateTime(2019, 12, 05) },
                new Player { FirstName = "Arjan", LastName = "Elmazi", DayOfBirth = new DateTime(2004, 05, 23), Gender = "Male", PhoneNo = "874-123-8791", Email = "arjanelmazi@hotmail.com", ClubId = Convert.ToInt32(boysU16.ClubId), ClubJoinedDate = new DateTime(2019, 12, 05) },
                new Player { FirstName = "Flori", LastName = "Meko", DayOfBirth = new DateTime(2003, 02, 06), Gender = "Male", PhoneNo = "874-321-8791", Email = "florimeko@hotmail.com", ClubId = Convert.ToInt32(boysU16.ClubId), ClubJoinedDate = new DateTime(2019, 12, 05) },
                new Player { FirstName = "Dean", LastName = "Meko", DayOfBirth = new DateTime(2008, 06, 09), Gender = "Male", PhoneNo = "874-123-1234", Email = "deanmeko@hotmail.com", ClubId = Convert.ToInt32(boysU12.ClubId), ClubJoinedDate = new DateTime(2019, 12, 05) }

                );
                _context.SaveChanges();
            }
        }
    }
}
