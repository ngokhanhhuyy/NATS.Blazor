using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NATS.Components;
using NATS.Helpers;
using NATS.Middlewares;
using NATS.Services;
using NATS.Services.Entities;
using NATS.Services.Identity;
using NATS.Services.Interfaces;
using NATS.Validation;
using FluentValidation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// Add database context.
string connectionString = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContextFactory<DatabaseContext>(options => options
    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    .AddInterceptors(new VietnamTimeInterceptor()));

// Add identity, for authentication and authorization.
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddErrorDescriber<VietnameseIdentityErrorDescriber>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 0;
});

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.LoginPath = "/SignIn";
    options.LogoutPath = "/SignOur";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme);

// FluentValidation.
builder.Services
    .AddValidatorsFromAssemblyContaining<NATS.Validation.Validators.SignInValidator>();
ValidatorOptions.Global.LanguageManager.Enabled = true;
ValidatorOptions.Global.LanguageManager = new ValidatorLanguageManager {
    Culture = new CultureInfo("vi")
};

// Dependency injection
builder.Services.AddSingleton<IRouteHelper, RouteHelper>();
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<RoleManager<Role>>();
builder.Services.AddScoped<DatabaseContext>();
builder.Services.AddScoped<
    NATS.Services.Interfaces.IAuthenticationService,
    NATS.Services.AuthenticationService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGeneralSettingsService, GeneralSettingsService>();
builder.Services.AddScoped<IAboutUsIntroductionService, AboutUsIntroductionService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<ISummaryItemService, SummaryItemService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<ICatalogItemService, CatalogItemService>();
builder.Services.AddScoped<ISliderItemService, SliderItemService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IEnquiryService, EnquiryService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ITrafficService, TrafficService>();

WebApplication app = builder.Build();
DataInitializer dataInitializer;
dataInitializer = new DataInitializer();
dataInitializer.InitializeData(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAntiforgery();
app.MapStaticAssets();
app.UseStaticFiles();
app.UseAuthentication();
app.Use(async (context, next) =>
{
    bool isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;
    if (isAuthenticated && context.Request.Path == "/SignIn")
    {
        context.Response.Redirect("/");
        return;
    }

    await next();
});
app.UseAuthorization();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();