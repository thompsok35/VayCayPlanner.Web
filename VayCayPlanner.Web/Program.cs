using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VayCayPlanner.Web.Data;
using VayCayPlanner.Web.Data.Models;
using VayCayPlanner.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//Identity Service
builder.Services.AddDefaultIdentity<Subscriber>(options => options.SignIn.RequireConfirmedAccount = true)
    //.AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
//Logging
builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IEmailSender>(s => new EmailSender("localhost", 25, "no-reply@vaycayplanner.com"));

//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
//builder.Services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
//builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

//builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
