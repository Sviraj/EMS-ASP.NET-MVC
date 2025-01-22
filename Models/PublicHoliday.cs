using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class PublicHoliday
{
    public int Id { get; set; }

    public DateOnly? HolidayDate { get; set; }

    public string? Description { get; set; }
}
