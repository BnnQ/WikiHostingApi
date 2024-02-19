using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WikiHostingApi.Contexts;
using WikiHostingApi.Entities;

namespace WikiHostingApi.Extensions;

public static class StartupExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<WikiHostingSqlServerContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString(name: "Default")));

        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<WikiHostingSqlServerContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = false;
            });

        #region Stub authentication settings for normal project startup
        
        builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                if (!builder.Environment.IsProduction())
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                }
                else
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                }
        
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(builder.Environment.IsProduction() ? 60 : 1);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
        
                // User settings
                options.User.RequireUniqueEmail = true;
            });
        
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(value: 60);
                
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
        
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });
        
            builder.Services.AddAuthentication()
                .AddCookie()
                // .AddGoogle(options =>
                // {
                //     options.ClientId = builder.Configuration[key: "Authentication:Google:ClientId"];
                //     options.ClientSecret = builder.Configuration[key: "Authentication:Google:ClientSecret"];
                //     options.SaveTokens = true;
                //     options.Scope.Add(PeopleServiceService.ScopeConstants.UserBirthdayRead);
                //     options.Events = new GoogleOAuthEvents();
                // })
                // .AddGitHub(options =>
                // {
                //     options.ClientId = builder.Configuration[key: "Authentication:GitHub:ClientId"];
                //     options.ClientSecret = builder.Configuration[key: "Authentication:GitHub:ClientSecret"];
                //     options.Scope.Add(item: "user:email");
                // });
                ;
        
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(name: "Authenticated", policy => policy.RequireAuthenticatedUser());
                options.AddPolicy(name: "AdminOnly", policy => policy.RequireAuthenticatedUser().RequireRole("Admin"));
            });
        
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = ".WikiHostingApi.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(value: 60);
            });
        
        #endregion
        
        return builder;
    }

    public static void Configure(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler(errorHandlingPath: "/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCookiePolicy();
        
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}