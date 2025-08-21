using System;
using EcommerceWebSite.Entity;
using EcommerceWebSite.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// --- Session ---
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opts =>
{
    opts.Cookie.Name = ".Ecommerce.Session";
    opts.IdleTimeout = TimeSpan.FromHours(2);
    opts.Cookie.HttpOnly = true;
    opts.Cookie.IsEssential = true;
});

// HttpContext accessor
builder.Services.AddHttpContextAccessor();

// --- DbContexts ---
builder.Services.AddDbContext<DataContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("dataConnection")));

builder.Services.AddDbContext<IdentityDataContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("identityConnection")));

// --- Identity ---
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(opts =>
{
    opts.Password.RequireDigit = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequiredLength = 6;
    opts.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<IdentityDataContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.LoginPath        = "/Account/Login";
    opts.AccessDeniedPath = "/Account/AccessDenied";
    opts.Cookie.Name      = "ApplicationCookie";
});

var app = builder.Build();

// ---- Veritabanı migrasyonlarını uygula (önerilir) ----
using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;

    var dataDb = sp.GetRequiredService<DataContext>();
    await dataDb.Database.MigrateAsync();

    var identityDb = sp.GetRequiredService<IdentityDataContext>();
    await identityDb.Database.MigrateAsync();
}

// ---- Seed verileri ----
await DataInitializer.SeedAsync(app.Services);
await IdentityInitializer.SeedAsync(app.Services);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session middleware
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
