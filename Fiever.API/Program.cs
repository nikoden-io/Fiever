// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Application.Services.AppServices;
using AutoMapper;
using Fiever.Application.Interfaces.ApplicationServices;
using Fiever.Application.Mappings;
using Fiever.Application.Services;
using Fiever.Domain.IRepositories;
using Fiever.Infrastructure.DbContext;
using Fiever.Infrastructure.Repositories;
using GoldenBack.API.Routing;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Logging
builder.Services.AddLogging(loggingBuilder => { loggingBuilder.AddConsole(); });

// Mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

// CORS Management
/***
 * localhost:5173 : Development Local Web Client
 * 127.0.0.1 : Localhost
 */
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins(
                    "http://localhost:5173", "https://localhost:5173",
                    "http://127.0.0.1")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// HTTP session 
builder.Services.AddSession(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Redis
builder.Services.AddDistributedMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:Configuration"];
});

builder.Services.AddEndpointsApiExplorer();

// Configure DbContext for EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the ADO.NET connection string for direct queries
builder.Services.AddSingleton(builder.Configuration.GetConnectionString("DefaultConnection"));


// Controllers injection
builder.Services.AddControllers(options => { options.Conventions.Add(new VersionRouteConvention()); });

// Application Services DI
builder.Services.AddScoped<ICountryAppService>(serviceProvider =>
{
    var countryRepository = serviceProvider.GetRequiredService<ICountryRepository>();
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    var logger = serviceProvider.GetRequiredService<ILogger<CountryAppService>>();
    return new CountryAppService(countryRepository, mapper, logger);
});

builder.Services.AddScoped<ICourseAppService>(serviceProvider =>
{
    var courseRepository = serviceProvider.GetRequiredService<ICourseRepository>();
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    var logger = serviceProvider.GetRequiredService<ILogger<CourseAppService>>();
    return new CourseAppService(courseRepository, mapper, logger);
});


builder.Services.AddScoped<IPassportAppService>(serviceProvider =>
{
    var passportRepository = serviceProvider.GetRequiredService<IPassportRepository>();
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    var logger = serviceProvider.GetRequiredService<ILogger<PassportAppService>>();
    return new PassportAppService(passportRepository, mapper, logger);
});

builder.Services.AddScoped<IStudentAppService>(serviceProvider =>
{
    var studentRepository = serviceProvider.GetRequiredService<IStudentRepository>();
    var countryRepository = serviceProvider.GetRequiredService<ICountryRepository>();
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    var logger = serviceProvider.GetRequiredService<ILogger<StudentAppService>>();
    return new StudentAppService(studentRepository, countryRepository, mapper, logger);
});

builder.Services.AddScoped<IStudentCourseAppService>(serviceProvider =>
{
    var studentCourseRepository = serviceProvider.GetRequiredService<IStudentCourseRepository>();
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    var logger = serviceProvider.GetRequiredService<ILogger<StudentCourseAppService>>();
    return new StudentCourseAppService(studentCourseRepository, mapper, logger);
});

builder.Services.AddScoped<IUniversityAppService>(serviceProvider =>
{
    var universityRepository = serviceProvider.GetRequiredService<IUniversityRepository>();
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    var logger = serviceProvider.GetRequiredService<ILogger<UniversityAppService>>();
    return new UniversityAppService(universityRepository, mapper, logger);
});


// Infrastructure Services DI

// Repository DI
builder.Services.AddScoped<ICourseRepository>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
    return new CourseRepository(connectionString, context);
});
builder.Services.AddScoped<ICountryRepository>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
    return new CountryRepository(connectionString, context);
});
builder.Services.AddScoped<IPassportRepository>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
    return new PassportRepository(connectionString, context);
});
builder.Services.AddScoped<IStudentCourseRepository>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new StudentCourseRepository(connectionString);
});
builder.Services.AddScoped<IUniversityRepository>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
    return new UniversityRepository(connectionString, context);
});


var app = builder.Build();

if (app.Environment.IsProduction()) app.UseHttpsRedirection();

app.MapControllers();
app.UseCors();
app.UseSession();


// Log routing for debug purpose
if (app.Environment.IsDevelopment())
    app.Use(async (context, next) =>
    {
        Console.WriteLine("Requested Path: " + context.Request.Path);
        await next();
    });


app.Run();

public partial class Program
{
}