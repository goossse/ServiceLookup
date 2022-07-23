using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL.Interfaces;
using ServiceLookup.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

//DB connection
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Manage}/{action=CreateService}/{id?}");

app.Run();
