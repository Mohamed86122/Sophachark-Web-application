using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Mappers;
using SophaTemp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUploadFileService, UploadFileService>();
builder.Services.AddScoped<CommandeMapper>();
/*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();*/
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPrincipalePolicy", policy =>
        policy.RequireClaim("Passeport", "AdminPrincipale"));
    options.AddPolicy("AdminCommandePolicy", policy =>
        policy.RequireClaim("Passeport", "AdminCommande"));
    options.AddPolicy("AdminProduitPolicy", policy =>
        policy.RequireClaim("Passeport", "AdminProduit"));
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
app.Run();
