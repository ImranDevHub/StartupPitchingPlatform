using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StartupPitchingPlatform.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; //Disable Require email confirmation
    options.Password.RequireDigit = true; // Require a digit in the password
    options.Password.RequiredLength = 8; // Minimum password length
    options.Password.RequireNonAlphanumeric = true; // Require special characters
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders() // Add this line
.AddDefaultUI(); // Add default Identity UI

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware setup
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Ensure this line is present
app.UseAuthorization();  // Ensure this line is present

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Ensure this line is present for Identity Razor Pages

app.Run();
