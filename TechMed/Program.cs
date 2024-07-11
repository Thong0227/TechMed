using Microsoft.EntityFrameworkCore;
using TechMed.Areas.Admin.Data;
using Microsoft.AspNetCore.Identity;
using TechMed.Data;
using TechMed.Service;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var settings = builder.Configuration
                .GetRequiredSection("ConnectionStrings"); //read data from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(settings["AppDbContext"]));

builder.Services.AddDbContext<TechMedContext>(options =>
    options.UseSqlServer(settings["AppDbContext"]));

builder.Services.AddDefaultIdentity<TechMed.Areas.Identity.Data.TechMedUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TechMedContext>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<NewsService>();
builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<BannerService>();
builder.Services.AddScoped<RecruitmentService>();
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();;

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapRazorPages();
app.Run();
