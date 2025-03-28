using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjektDT191G.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

// Skapa roller och användare i scope med roleManager
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Skapa roller
    var roles = new[] { "Administrator", "Speaker" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Skapa användare med userManager
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var users = new[] {
        new { Email = "adkn@student.miun.se", Password = "ProjektDT191G!", Role = "Administrator"},
        new { Email = "adela@miun.se", Password = "ProjektDT191G!", Role = "Speaker"},
    };

    foreach (var user in users)
    {
        var identityUser = await userManager.FindByEmailAsync(user.Email);
        if (identityUser == null)
        {
            identityUser = new IdentityUser { UserName = user.Email, Email = user.Email };
            await userManager.CreateAsync(identityUser, user.Password);

            // Tilldela roll
            await userManager.AddToRoleAsync(identityUser, user.Role);
        }
    }
}

app.Run();
