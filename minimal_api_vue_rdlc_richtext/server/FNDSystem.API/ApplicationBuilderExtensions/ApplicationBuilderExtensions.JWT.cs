namespace FNDSystem.API.ApplicationBuilderExtensions;
internal static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Register JWT Authentication + Authorization.
    /// </summary>
    public static IApplicationBuilder UseJWTAuthorization(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}