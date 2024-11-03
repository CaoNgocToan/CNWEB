using ITShop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ITShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ITShopConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//x�c th?c
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "ITShop.Cookie";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(15); //gia h?n ??ng nh?p 
        options.SlidingExpiration = true;
        options.LoginPath = "/Home/Login";
        options.LogoutPath = "/Home/Logout";
        options.AccessDeniedPath = "/Home/Forbidden";
    } 
    );
builder.Services.AddHttpContextAccessor();

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

//add ph?i c� th�m h�m s? d?ng
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "adminareas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
