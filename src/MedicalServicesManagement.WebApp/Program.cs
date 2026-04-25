using AutoMapper;
using MedicalServicesManagement.BLL;
using MedicalServicesManagement.BLL.Jwt;
using MedicalServicesManagement.BLL.Mapper;
using MedicalServicesManagement.WebApp.Extensions;
using MedicalServicesManagement.WebApp.Jwt;
using MedicalServicesManagement.WebApp.Mapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

const string MedServiceConnectionString = "MedDB";
const string AuthConnectionString = "AuthDB";
const string MongoConnectionString = "MongoDB";
const string JwtTokenSettings = "JwtTokenSettings";

var builder = WebApplication.CreateBuilder(args);

var medServiceConnection = builder.Configuration.GetConnectionString(MedServiceConnectionString)
    ?? throw new ArgumentException($"{MedServiceConnectionString} is null. Error connection.");
var authConnection = builder.Configuration.GetConnectionString(AuthConnectionString)
    ?? throw new ArgumentException($"{AuthConnectionString} is null. Error connection.");
var mongoConnection = builder.Configuration.GetConnectionString(MongoConnectionString)
    ?? throw new ArgumentException($"{MongoConnectionString} is null. Error connection.");

var strings = new Dictionary<string, string>
{
    [MedServiceConnectionString] = medServiceConnection,
    [AuthConnectionString] = authConnection,
    [MongoConnectionString] = mongoConnection,
};

var mongoDbName = builder.Configuration.GetSection("mongoDbName").Value;

builder.Services.ConfigureBLL(strings, mongoDbName);

var mapperConfig = new MapperConfiguration(
    cfg =>
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
    app.UseExceptionHandler("/Home/Error"); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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