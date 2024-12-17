using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using StudentsForStudents.Context;
using StudentsForStudents.Models;
using System.Globalization;
using SmartLineSystem.Models;
using StudentsForStudents.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SFSDBContect>(option => option.UseMySQL(builder.Configuration.GetConnectionString("mysqlconnect")));
builder.Services.AddLocalization();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    // Set session timeout to 30 minutes
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddSignalR()
        .AddHubOptions<ChatHub>(options =>
        {
            options.EnableDetailedErrors = true;
        });

// Configure SmtpSettings from appsettings.json
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));


//builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
//builder.Services.AddMvc()
//    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
//    .AddDataAnnotationsLocalization(options =>
//    {
//        options.DataAnnotationLocalizerProvider = (type, factory) =>
//          factory.Create(typeof(JsonStringLocalizerFactory));
//    });

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    var supportedCultuers = new[]
//    {
//        new CultureInfo("en"),
//        new CultureInfo("ar"),
//    };
//
//    options.DefaultRequestCulture = new RequestCulture(culture: supportedCultuers[0], uiCulture: supportedCultuers[0]);
//    options.SupportedCultures = supportedCultuers;
//    options.SupportedUICultures = supportedCultuers;
//});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Use CookieAuthenticationDefaults.AuthenticationScheme for appCookie
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "StudentsForStudentsCookie"; // Set your cookie name
        options.Cookie.HttpOnly = true; // HTTP only cookie, improves security
        options.Cookie.SameSite = SameSiteMode.Strict; // SameSite protection
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Use 'None' for development without HTTPS
        options.Cookie.IsEssential = true; // Cookie is essential for authentication

        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Cookie expiration time
        options.SlidingExpiration = true; // Extend expiration on activity
    });

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<SFSDBContect>()
.AddDefaultTokenProviders();


var app = builder.Build();
app.UseSession();
app.MapHub<ChatHub>("/chatHub");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//var supportedCultuers = new[] { "en", "ar" };
//var localizationOptions = new RequestLocalizationOptions()
//    .SetDefaultCulture(supportedCultuers[0])
//    .AddSupportedCultures(supportedCultuers)
//    .AddSupportedUICultures(supportedCultuers);
//
//app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");
app.MapPost("/Login", async (HttpContext context) =>
{
    var Email = context.Request.Form["Email"];
    var Password = context.Request.Form["Password"];

    // Your login logic here...

    // Store user email in session
    context.Session.SetString("UserEmail", Email);

    // Redirect to the index page
    context.Response.Redirect("/");
});

app.Run();
