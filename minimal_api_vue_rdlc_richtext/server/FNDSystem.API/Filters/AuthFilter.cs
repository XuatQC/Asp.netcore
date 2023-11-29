using FNDSystem.Core.Dto;
using FNDSystem.Core.Helpers;

namespace FNDSystem.Core.Filters;

public class AuthFilter : IEndpointFilter
{

    private readonly Serilog.ILogger logger;

    public AuthFilter(Serilog.ILogger logger)
    {
        this.logger = logger;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        context.HttpContext.Items[nameof(AuthenticatedUserDto)] = null;
        if (!string.IsNullOrEmpty(token))
        {
            token = token.Replace("Bearer ", string.Empty);
            var tokenData = StringUtils.VerifyJWTToken(token);
            if (tokenData is not null)
            {
                context.HttpContext.Items[nameof(AuthenticatedUserDto)] = new AuthenticatedUserDto
                {
                    Name = tokenData.Claims.First(x => x.Type == "name").Value,
                    Initial = tokenData.Claims.First(x => x.Type == "Initial").Value,
                    //Email = tokenData.Claims.First(x => x.Type == "Email").Value,
                    //Code = tokenData.Claims.First(x => x.Type == "Code").Value,
                    //TenantCode = tokenData.Claims.First(x => x.Type == "TenantCode").Value,
                    //RoleKbn = tokenData.Claims.First(x => x.Type == "RoleKbn").Value,
                };

                return await next(context);
            }
        }

        return Results.Ok(new
        {
            Code = 401,
            Message = "不正なアクセスです。"
        });
        // OR Results.Unauthorized
    }
}