using WebApplication1.Interfaces;
using WebApplication1.Models;
using System;
using System.Linq;

namespace WebApplication1.Services
{
    public class WorkingDaysService : IWorkingDaysService
    {
        private readonly IRepository<PublicHoliday> _holidayRepository;

        public WorkingDaysService(IRepository<PublicHoliday> holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }

        public int CalculateWorkingDays(DateTime start, DateTime end
            //, CacheHelper cacheHelper
            )
        {
            // Ensure the start date is not Saturday or Sunday
            //if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
            //{
            //    throw new ArgumentException("Start date cannot be on a weekend.");
            //}

            // Convert your holidays to a list of DateTime first
            var holidayDates = _holidayRepository.GetAll()
                .Select(h => h.HolidayDate)       // h.HolidayDate must be DateTime (not null)
                .ToList();

            int workingDays = 0;
            for (var d = start.Date; d <= end.Date; d = d.AddDays(1))
            {
                // Convert the DateTime 'd' to a DateOnly
                var dDateOnly = DateOnly.FromDateTime(d);

                // Exclude weekends
                if (d.DayOfWeek != DayOfWeek.Saturday &&
                    d.DayOfWeek != DayOfWeek.Sunday &&
                    // Now compare DateOnly to DateOnly
                    !holidayDates.Contains(dDateOnly))
                {
                    workingDays++;
                }
            }

            return workingDays;
        }
    }

}
