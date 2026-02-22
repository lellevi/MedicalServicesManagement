using AutoMapper;
using MedicalServicesManagement.BLL;
using MedicalServicesManagement.BLL.Jwt;
using MedicalServicesManagement.BLL.Mapper;
using MedicalServicesManagement.DAL.Contexts;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.WebApp.Extensions;
using MedicalServicesManagement.WebApp.Jwt;
using MedicalServicesManagement.WebApp.Mapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

const string MedServiceConnectionString = "MedDB";
const string AuthConnectionString = "AuthDB";
const string JwtTokenSettings = "JwtTokenSettings";

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var medServiceConnection = builder.Configuration.GetConnectionString(MedServiceConnectionString)
    ?? throw new ArgumentNullException(nameof(MedServiceConnectionString));
var authConnection = builder.Configuration.GetConnectionString(AuthConnectionString)
                ?? throw new ArgumentNullException(nameof(AuthConnectionString));

var strings = new Dictionary<string, string>
{
    [MedServiceConnectionString] = medServiceConnection,
    [AuthConnectionString] = authConnection
};

builder.Services.ConfigureBLL(strings);

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<BLLAutomapperProfile>();
    cfg.AddProfile<AutomapperProfile>();
}, new LoggerFactory());
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.RegisterOptions<JwtTokenSettings>(JwtTokenSettings);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, JwtAuthenticationHandler>(
        JwtBearerDefaults.AuthenticationScheme, null);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();