using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Helpers;
using System;
using System.Linq;

namespace WebApplication1.Services
{
    public class WorkingDaysService : IWorkingDaysService
    {
        private readonly IRepository<PublicHoliday> _holidayRepository;
        private readonly IMemoryCacheHelper _memoryCacheHelper;

        public WorkingDaysService(IRepository<PublicHoliday> holidayRepository, IMemoryCacheHelper memoryCacheHelper)
        {
            _holidayRepository = holidayRepository;
            _memoryCacheHelper = memoryCacheHelper;
        }

        public int CalculateWorkingDays(DateTime start, DateTime end)
        {
            //Ensure the start date is not Saturday or Sunday
            if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new ArgumentException("Start date cannot be on a weekend.");
            }

            var holidays = _memoryCacheHelper.CachedLong("Holidays", 
                () => _holidayRepository.GetAll().Select(h => h.HolidayDate).ToList());
            // Convert your holidays to a list of DateTime first
            //var holidayDates = _holidayRepository.GetAll()
            //    .Select(h => h.HolidayDate)       // h.HolidayDate must be DateTime (not null)
            //    .ToList();

            int workingDays = 0;
            for (var d = start.Date; d <= end.Date; d = d.AddDays(1))
            {
                // Convert the DateTime 'd' to a DateOnly
                var dDateOnly = DateOnly.FromDateTime(d);

                // Exclude weekends
                if (d.DayOfWeek != DayOfWeek.Saturday &&
                    d.DayOfWeek != DayOfWeek.Sunday &&
                    // Now compare DateOnly to DateOnly
                    !holidays.Contains(dDateOnly))
                {
                    workingDays++;
                }
            }

            return workingDays;
        }
    }

}
