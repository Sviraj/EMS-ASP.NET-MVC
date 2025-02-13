using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) Register the DbContext
builder.Services.AddDbContext<EmsAspmvcContext>(options =>
{
    // E.g. if using SQL Server:
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// For ASP.NET Core in Program.cs:
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();


// Add services to the container.
builder.Services.AddControllersWithViews();

// 2) Register your Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IWorkingDaysService, WorkingDaysService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
