using MedicalServicesManagement.BLL;
using MedicalServicesManagement.BLL.Jwt;
using MedicalServicesManagement.DAL.Contexts;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.WebApp.Extensions;
using MedicalServicesManagement.WebApp.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

//const string MedConnectionString = "MedDB";
const string AuthConnectionString = "AuthDB";
const string JwtTokenSettings = "JwtTokenSettings";

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var authConnection = builder.Configuration.GetConnectionString(AuthConnectionString)
                ?? throw new ArgumentNullException(AuthConnectionString);

var strings = new Dictionary<string, string>
{
    [AuthConnectionString] = authConnection
};

builder.Services.ConfigureBLL(strings);

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