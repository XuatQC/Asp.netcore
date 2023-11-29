using Modules.Shared;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Core.Models;
using FluentValidation;
using FluentValidation.Results;
using Core.Filters;

public class AuthModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/auth/login", LoginHandler).Produces<LoginResponseDto>(200).WithName("Login").WithTags("Auth");
        endpoints.MapGet("/auth/me", GetMeHandler).Produces<LoginResponseDto>(200).AddEndpointFilter<AuthFilter>().WithName("GetMe").WithTags("Auth");
        return endpoints;
    }

    private IResult LoginHandler(LoginRequestDto dto, [FromServices] IAuthService authService, IValidator<LoginRequestDto> validator)
    {
        ValidationResult result = validator.Validate(dto);

        if (result.IsValid)
        {
            LoginResponseDto loginResponseDto = authService.Login(dto.Email, dto.Password);

            return Results.Ok(new R<LoginResponseDto>()
            {
                Payload = loginResponseDto
            });
        }
        else
        {
            return Results.BadRequest(new R<LoginResponseDto>()
            {
                Code = 400,
                Message = result.Errors.ElementAt(0).ErrorMessage,
            });
        }
    }

    private IResult GetMeHandler([FromServices] IAuthService authService)
    {
        LoginResponseDto loginResponseDto = authService.GetMe();

        return Results.Ok(new R<LoginResponseDto>()
        {
            Payload = loginResponseDto
        });
    }
}