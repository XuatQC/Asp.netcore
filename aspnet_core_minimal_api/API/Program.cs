using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;
using API.ApplicationBuilderExtensions;
using API.ServiceCollectionExtensions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Configuration;
using Core.Exceptions;
using Core.Helpers.AppSettings;
using Core.Helpers.Seeder;
using Core.Middlewares;
using Core.Models;
using FluentValidation;
using Infrastructure;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.AddSwagger()
       .AddJWTAuthorization();
builder.Services.RegisterModules();

var POSTGRES = builder.Configuration.GetSection("Databases:Postgres").Get<PostgresDB>();
var LOGGER = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {MachineName}] {Email} {Message:lj}{NewLine}{Exception}")
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

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContextPool<DBMasterContext>(options =>
{
    options.UseNpgsql(POSTGRES!.Master);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.LogTo(builder.Environment.IsProduction() ? LOGGER.Error : LOGGER.Information);
    options.EnableSensitiveDataLogging(true);
});
builder.Services.AddDbContextPool<DBSlaveContext>(options =>
{
    options.UseNpgsql(POSTGRES!.Slave);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.LogTo(builder.Environment.IsProduction() ? LOGGER.Error : LOGGER.Information);
    options.EnableSensitiveDataLogging(true);
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();
var environment = app.Environment;

app.UseExceptionHandling(environment)
    .UseSwaggerEndpoints(routePrefix: string.Empty)
    .UseJWTAuthorization();

app.MapEndpoints();

// Only seeding if it is not production
if (!builder.Environment.IsProduction())
{
    Seeder seeder = new Seeder(POSTGRES!.Master);
    seeder.Seed(10);
}

app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is ControlledException)
            {
                var exception = exceptionHandlerPathFeature?.Error as ControlledException;
                if (exception is not null)
                {
                    await Results.Ok(new
                    {
                        Code = exception.ErrorCode,
                        Message = exception.ErrorMessage
                    }).ExecuteAsync(context);
                }
                else
                {
                    await Results.Problem().ExecuteAsync(context);
                }
            }
            else
            {
                await Results.Problem().ExecuteAsync(context);
            }
        });
    });

app.UseMiddleware<HttpLoggingMiddleware>();

app.Run();
