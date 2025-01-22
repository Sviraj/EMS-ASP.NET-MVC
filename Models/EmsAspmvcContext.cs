using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class EmsAspmvcContext : DbContext
{
    public EmsAspmvcContext()
    {
    }

    public EmsAspmvcContext(DbContextOptions<EmsAspmvcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<PublicHoliday> PublicHolidays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-PMPBIGB\\SQLEXPRESS;Database=EmsASPMVC;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07230E76A5");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.JobPosition).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<PublicHoliday>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PublicHo__3214EC07ADC6367C");

            entity.Property(e => e.Description).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
