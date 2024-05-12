using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Filter;
using SophaTemp.Mappers;
using SophaTemp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Ajout des services MVC et du filtre d'action personnalisé
///////////////////
//builder.Services.AddControllersWithViews(options =>
//{
//    options.Filters.Add<PasseportAuthorizationFilter>(); // Enregistrement global du filtre
//});
//builder.Services.AddScoped<PasseportAuthorizationFilter>();
////////////////////
/*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();*/
/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPrincipalePolicy", policy =>
        policy.RequireClaim("Passeport", "AdminPrincipale"));
    options.AddPolicy("AdminCommandePolicy", policy =>
        policy.RequireClaim("Passeport", "AdminCommande"));
    options.AddPolicy("AdminProduitPolicy", policy =>
        policy.RequireClaim("Passeport", "AdminProduit"));
});*/
///////////////////
//builder.Services.AddScoped<IUploadFileService, UploadFileService>();
//builder.Services.AddScoped<CommandeMapper>();
//builder.Services.AddScoped<PersonMapper>();
//////////////////////
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
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Route pour rediriger /Admin vers /Admin/Login/Login
    endpoints.MapControllerRoute(
        name: "adminLoginShortcut",
        pattern: "Admin",
        defaults: new { area = "Admin", controller = "Login" });

    // Configuration standard des routes d'area
    endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    // Route par défaut
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
app.Run();
