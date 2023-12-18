using KPZ_React_Lab.Data;
using KPZ_React_Lab.Services.Interfaces;
using KPZ_React_Lab.Services;
using KPZ_React_Lab.Mappings;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProjectsService, ProjectsService>();
builder.Services.AddTransient<IEmployeesService, EmployeesService>();
builder.Services.AddSingleton<TeamManagementDataContext>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
     app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
