using GuitarShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QQVault.Data;
using QQVault.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Register repositories and the repository wrapper
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

// Add identity and configure DbContext
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<BankDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<BankDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false; // Set to true if you want to require digits
    options.Password.RequireLowercase = false; // Set to true if you want to require lowercase
    options.Password.RequireUppercase = false; // Set to true if you want to require uppercase
    options.Password.RequireNonAlphanumeric = false; // Set to true if you want to require special characters
    options.Password.RequiredLength = 6; // Minimum length of the password
    // Other options...
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

var app = builder.Build();

// Middleware configuration
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

SeedData.EnsurePopulated(app);

app.Run();
