using Microsoft.AspNetCore.Authentication.Cookies;
using UniversityPortal.BLL.Interfaces;
using UniversityPortal.BLL.Services;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.DAL.Repositories;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

if (Uri.TryCreate(builder.Configuration["AppConfig"], UriKind.Absolute, out var endpoint))
{
    // Use Azure Active Directory authentication.
    // The identity of this app should be assigned 'App Configuration Data Reader' or 'App Configuration Data Owner' role in App Configuration.
    // For more information, please visit https://aka.ms/vs/azure-app-configuration/concept-enable-rbac
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(endpoint, new DefaultAzureCredential())
        .ConfigureRefresh(refresh =>
        {
            // All configuration values will be refreshed if the sentinel key changes.
            refresh.Register("TestApp:Settings:Sentinel", refreshAll: true);
        });
    });
}
builder.Services.AddAzureAppConfiguration();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// DAL
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();
builder.Services.AddScoped<IAssessmentRepository, AssessmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// BLL
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<IAssessmentService, AssessmentService>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseAzureAppConfiguration();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
