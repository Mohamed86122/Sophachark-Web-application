using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Services;
using SophaTemp.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Configuration des services
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUploadFileService, UploadFileService>();
builder.Services.AddScoped<CommandeMapper>();
builder.Services.AddScoped<PersonMapper>();

var app = builder.Build();

// Configuration du pipeline de requêtes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Configuration des endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "adminLoginShortcut",
        pattern: "Admin",
        defaults: new { area = "Admin", controller = "Login", action = "Login" });
    endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
