using Bloggie.Web.Controllers;
using CloudinaryDotNet.Actions;
using DB;
using DB.Context;
using DB.IRepository;
using DB.Model;
using DB.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddAuthorization(opt => 
{
    opt.AddPolicy("Admin", policy => policy.RequireRole(SD.Admin));
});


builder.Services.Configure<SMTP>(builder.Configuration.GetSection("Brevo"));
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IImageRepostiory, ImageRepository>();
builder.Services.AddDbContext<BlogDB>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("Defaoult"), b => b.MigrationsAssembly("DB")));
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IEmailServise, EmailServise>();
builder.Services.AddDbContext<AuthDb>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("AuthDb")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AuthDb>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    
    opt.Lockout.MaxFailedAccessAttempts = 3;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);      
    opt.SignIn.RequireConfirmedEmail = true;

});
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.AccessDeniedPath = new PathString("/User/AccessDeniedPage");
    opt.LoginPath = new PathString("/User/Login");

});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.Run();
