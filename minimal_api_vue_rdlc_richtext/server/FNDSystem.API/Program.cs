using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;
using FNDSystem;
using FNDSystem.API.ApplicationBuilderExtensions;
using FNDSystem.API.Configuration;
using FNDSystem.API.ServiceCollectionExtensions;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Domain.RepositoryContracts;
using FNDSystem.Core.Dto;
using FNDSystem.Core.Exceptions;
using FNDSystem.Core.Helpers;
using FNDSystem.Core.Middlewares;
using FNDSystem.Core.Services;
using FNDSystem.Infrastructure;
using FNDSystem.Infrastructure.DataAccess.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Modules.Shared;
using Serilog;
using Serilog.Events;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var JWTTOKENBASE = builder.Configuration.GetSection("Jwt").Get<AuthTokenBase>();
if (JWTTOKENBASE != null) StringUtils.SetAuthTokenBase(JWTTOKENBASE);

builder.AddSwagger()
       .AddJWTAuthorization();
builder.Services.RegisterModules();

var MYSQL = builder.Configuration.GetSection("Databases:Mysql").Get<MysqlDB>();

string ALLOW_CORS_ORIGINS = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string>() ?? "*";

long fileLogSize = 2097152; // 2MB

var LOGGER = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(path: "logs/Debug/Log_.txt",
                            restrictedToMinimumLevel: LogEventLevel.Debug,
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true,
                            fileSizeLimitBytes: fileLogSize)
            .WriteTo.File(path: "logs/Info/Log_.txt",
                            restrictedToMinimumLevel: LogEventLevel.Information,
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true,
                            fileSizeLimitBytes: fileLogSize)
            .WriteTo.File(path: "logs/Error/Log_.txt",
                            restrictedToMinimumLevel: LogEventLevel.Error,
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true,
                            fileSizeLimitBytes: fileLogSize)
            .CreateLogger();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    // Add your Autofac DI registrations here

    // Service: automatically find all the validators in a specific assembly
    builder.RegisterAssemblyTypes(typeof(AuthService).Assembly)
   .PublicOnly()
   .Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

    // Repository: automatically find all the validators in a specific assembly
    builder.RegisterAssemblyTypes(typeof(AccountRepository).Assembly)
   .PublicOnly()
   .Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

    builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

    builder.Register(context => new MapperConfiguration(cfg =>
    {
        //Register Mapper Profile
        cfg.AddProfile<AutoMapperProfile>();
    }
    )).AsSelf().SingleInstance();

    builder.Register(c =>
    {
        var context = c.Resolve<IComponentContext>();
        var config = context.Resolve<MapperConfiguration>();
        return config.CreateMapper(context.Resolve);
    })
    .As<IMapper>()
    .InstancePerLifetimeScope();

    // FluentValidation : automatically find all the validators in a specific assembly
    builder.RegisterAssemblyTypes(typeof(LoginRequestDtoValidator).Assembly)
       .PublicOnly()
       .Where(t => t.Name.EndsWith("Validator")).AsImplementedInterfaces();
    // FluentValidation: Localization ( Japanese )
    ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("ja");

    // Serilog
    builder.Register<Serilog.ILogger>((c, p) =>
    {
        return LOGGER;
    }).SingleInstance();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(option =>
    {
        bool isAllowAll = ALLOW_CORS_ORIGINS?.Contains("*") ?? false;
        if (isAllowAll)
        {
            option.SetIsOriginAllowed(isOriginAllowed => _ = true)
            .WithHeaders("Authorization", "origin", "accept", "content-type", "RequestHeaderDto")
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .AllowCredentials();
        }
        else
        {
            string[] origins = ALLOW_CORS_ORIGINS!.Split(",");
            option.WithOrigins(origins)
            .WithHeaders("Authorization", "origin", "accept", "content-type", "RequestHeaderDto")
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .AllowCredentials();
        }
    });
});


builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContextPool<DBMasterContext>(options =>
{
    options.UseMySQL(MYSQL!.Master);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.LogTo(LOGGER.Information);
    options.EnableSensitiveDataLogging(true);
});
builder.Services.AddDbContextPool<DBSlaveContext>(options =>
{
    options.UseMySQL(MYSQL!.Slave);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.LogTo(LOGGER.Information);
    options.EnableSensitiveDataLogging(true);
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();
var environment = app.Environment;

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();

app.UseExceptionHandling(environment);
app.MapEndpoints();
app.UseJWTAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseDeveloperExceptionPage();

if (builder.Environment.IsProduction())
{
    app.MapFallbackToFile("index.html");
}
else
{
    app.UseSwaggerEndpoints(routePrefix: string.Empty);
}

app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;
            LOGGER.Error(exception?.Message + Environment.NewLine + exception?.StackTrace);
            if (exception is ControlledException)
            {
                var controlledException = exception as ControlledException;
                if (controlledException is not null)
                {
                    await Results.Ok(new
                    {
                        Code = controlledException.ErrorCode,
                        Message = controlledException.ErrorMessage
                    }).ExecuteAsync(context);
                }
                else
                {
                    await Results.Problem().ExecuteAsync(context);
                }
            }
            else
            {
                await Results.Problem(exceptionHandlerPathFeature?.Error.Message).ExecuteAsync(context);
            }
        });
    });

app.UseMiddleware<HttpLoggingMiddleware>();

app.Run();
