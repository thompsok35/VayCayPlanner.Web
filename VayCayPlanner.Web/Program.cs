using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VayCayPlanner.Data;
using VayCayPlanner.Data.Models;
using VayCayPlanner.Data.Repositories;
using VayCayPlanner.Data.Repositories.Contracts;
using VayCayPlanner.Web.Services;
using VayCayPlanner.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
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

var SMTPconfig = builder.Configuration;
builder.Services.AddTransient<IEmailSender>(s => new EmailSender("smtp-relay.sendinblue.com", 587, "no-reply@vaycayplanner.com", SMTPconfig));

//TODO: Register the repositories
builder.Services.AddScoped<ITravelGroupRepository, TravelGroupRepository>();
builder.Services.AddScoped<ITravelerRepository, TravelerRepository>();



builder.Services.AddAutoMapper(typeof(MapperConfig));

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
