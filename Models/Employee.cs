using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? JobPosition { get; set; }
}
