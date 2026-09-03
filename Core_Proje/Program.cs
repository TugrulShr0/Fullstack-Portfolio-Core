using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı Bağlantısı (appsettings.json'dan)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));

// 2. Identity Servisi
builder.Services.AddIdentity<WriterUser, WriterRole>(options =>
{
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+şŞıİğĞüÜöÖçÇ";
})
.AddEntityFrameworkStores<Context>()
.AddDefaultTokenProviders();

// 3. IoC Container - Dependency Injection Kayıtları (Native DI)
builder.Services.AddScoped<IAboutDal, EfAboutDal>();
builder.Services.AddScoped<IAboutService, AboutManager>();

builder.Services.AddScoped<IAnnouncementDal, EfAnnouncementDal>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementManager>();

builder.Services.AddScoped<IContactDal, EfContactDal>();
builder.Services.AddScoped<IContactService, ContactManager>();

builder.Services.AddScoped<IExperienceDal, EfExperienceDal>();
builder.Services.AddScoped<IExperienceService, ExperienceManager>();

builder.Services.AddScoped<IFeatureDal, EfFeatureDal>();
builder.Services.AddScoped<IFeatureService, FeatureManager>();

builder.Services.AddScoped<IMessageDal, EfMessageDal>();
builder.Services.AddScoped<IMessageService, MessageManager>();

builder.Services.AddScoped<IPortfolioDal, EfPortfolioDal>();
builder.Services.AddScoped<IPortfolioService, PortfolioManager>();

builder.Services.AddScoped<IServiceDal, EfServiceDal>();
builder.Services.AddScoped<IServiceService, ServiceManager>();

builder.Services.AddScoped<ISkillDal, EfSkillDal>();
builder.Services.AddScoped<ISkillService, SkillManager>();

builder.Services.AddScoped<ISocialMediaDal, EfSocialMediaDal>();
builder.Services.AddScoped<ISocialMediaService, SocialMediaManager>();

builder.Services.AddScoped<ITestimonialDal, EfTestimonialDal>();
builder.Services.AddScoped<ITestimonialService, TestimonialManager>();

builder.Services.AddScoped<IToDoListDal, EfToDoListDal>();
builder.Services.AddScoped<IToDoListService, ToDoListManager>();

builder.Services.AddScoped<IWriterMessageDal, EfWriterMessageDal>();
builder.Services.AddScoped<IWriterMessageService, WriterMessageManager>();

builder.Services.AddScoped<IWriterUserDal, EfWriterUserDal>();
builder.Services.AddScoped<IWriterUserService, WriterUserManager>();

// 4. HTTP Client ve MVC Ayarları
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

// 5. Cookie Ayarları
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(100);
    options.AccessDeniedPath = "/ErrorPage/Index/";
    options.LoginPath = "/Writer/Login/Index/";
});

var app = builder.Build();

// Pipeline Yapılandırması (.NET 8 Minimal Pipeline)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/ErrorPage/Index");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404/");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
