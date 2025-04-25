using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizDishtv.Data;
using QuizDishtv.Models;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QuizDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<User>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false;
//})
//.AddRoles<IdentityRole>() // Add this line
//.AddEntityFrameworkStores<QuizDbContext>();
builder.Services.AddMvc();

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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    string[] roles = ["Admin", "User"];

//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }
//    }

//    // Optional: Create Admin User
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
//    string adminEmail = "admin@quiz.com";
//    string adminPassword = "Admin@123";

//    if (await userManager.FindByEmailAsync(adminEmail) == null)
//    {
//        var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
//        var result = await userManager.CreateAsync(adminUser, adminPassword);
//        if (result.Succeeded)
//        {
//            await userManager.AddToRoleAsync(adminUser, "Admin");
//        }
//    }
//    //}

    app.Run();

