using Modules.Shared;
using Core.Entities;
using Core.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Core.Filters;

public class AccountModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var account = endpoints.MapGroup("/accounts")
                        .AddEndpointFilter<AuthFilter>()
                        .WithTags("Accounts");
        ;
        account.MapGet("", FindManyHandler).RequireAuthorization("Owner").Produces<IEnumerable<Account>>(200);
        account.MapPost("", CreateHandler).RequireAuthorization("Owner").Produces<Account>(200);

        return endpoints;
    }

    private R<IEnumerable<Account>> FindManyHandler([AsParameters] FindAccountRequestDto findAccountDto, [FromServices] IAccountService accountService)
    {
        var response = accountService.FindMany(findAccountDto);

        return new R<IEnumerable<Account>>()
        {
            Payload = response
        };
    }

    private IResult CreateHandler(CreateAccountRequestDto accountDto, [FromServices] IAccountService accountService, IValidator<CreateAccountRequestDto> validator)
    {
        ValidationResult result = validator.Validate(accountDto);

        if (result.IsValid)
        {
            var response = accountService.Create(accountDto);

            return Results.Ok(new R<Account>()
            {
                Payload = response
            });
        }
        else
        {
            return Results.BadRequest(new R<Account>()
            {
                Code = 400,
                Message = result.Errors.ElementAt(0).ErrorMessage,
            });
        }

    }
}