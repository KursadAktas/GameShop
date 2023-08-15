using GameShop.Business.Managers;
using GameShop.Business.Services;
using GameShop.Data.Context;
using GameShop.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GameShopContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUserService, UserManager>(); //Dependency Injection yapýlmasý ile kullanýlacak
builder.Services.AddScoped(typeof(IRepository<>),typeof(SqlRepository<>));
builder.Services.AddScoped<IUserService, UserManager>();

builder.Services.AddDataProtection(); //cripto þifre ekleme

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<ITestimonialService, TestimonialManager>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/"); //loginde nereye göndersin.
    options.LogoutPath = new PathString("/");
    options.AccessDeniedPath = new PathString("/");
    // giriþ ve çýkýþ eriþim reddi durumlarýnda ana sayfaya yönlendiriyor.

});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(); // www.root kullanýlacak

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}" //area pattern aþaðýdakinin üzerinde olmalý
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.Run();
