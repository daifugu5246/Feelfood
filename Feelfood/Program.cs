using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Feelfood.Data;
using Feelfood.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("FeelfoodDbContextConnection") ?? throw new InvalidOperationException("Connection string 'FeelfoodDbContextConnection' not found.");

builder.Services.AddDbContext<FeelfoodDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<FeelfoodUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<FeelfoodDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
