using Logic.DbContext;
using Logic.Models.Interfaces;
using Logic.Models;
using Logic.Repository;
using Microsoft.EntityFrameworkCore;
using Logic.Infrastructure;
using Microsoft.AspNetCore.Identity;
using API.ApiExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseInMemoryDatabase("UserDb");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAccountManager, AccountManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();


//builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddApiAuthentication("secretKeySecretKeySecretKey123456789");

// builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Добавление свагера 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseSession();
//app.UseMiddleware<TokenMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.MapControllers();
app.MapControllerRoute(
name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();