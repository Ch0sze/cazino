using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using cazino3.Areas.Identity.Data;
using UTB.Eshop.Application.Abstractions;
using UTB.Eshop.Application.Implementation;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DBcazinoContextConnection") ?? throw new InvalidOperationException("Connection string 'DBcazinoContextConnection' not found.");

builder.Services.AddDbContext<DBcazinoContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<cazinoUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<DBcazinoContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBalanceService, BalanceService>();

builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
});

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
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
