using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using osprey_web_app.Areas.Identity.Data;
using osprey_web_app.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("osprey_web_appContextConnection")
    ?? throw new InvalidOperationException("Connection string 'osprey_web_appContextConnection' not found.");

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<osprey_web_appContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity
builder.Services.AddDefaultIdentity<osprey_web_appUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // No email confirmation required for login
})
.AddEntityFrameworkStores<osprey_web_appContext>();

// Configure Authentication Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";  // Redirect unauthenticated users
    options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Redirect unauthorized users
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Session timeout
    options.SlidingExpiration = true;
});

// Add controllers and views
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Ensure database updates are applied
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<osprey_web_appContext>();
    dbContext.Database.Migrate(); // Apply migrations on startup
}

// Middleware configuration
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Use DeveloperExceptionPage in Development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Strict HTTP headers
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Enable authentication
app.UseAuthorization();  // Enable authorization

// Ensure Identity pages (Login, Register) are mapped before controllers
app.MapRazorPages();

// Default route configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
