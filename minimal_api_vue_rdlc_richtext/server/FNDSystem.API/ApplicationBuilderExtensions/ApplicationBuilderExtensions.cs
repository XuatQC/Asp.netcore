namespace FNDSystem.API.ApplicationBuilderExtensions;
internal static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Register exception handling.
    /// </summary>
    public static IApplicationBuilder UseExceptionHandling(
        this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        return app;
    }

    /// <summary>
    /// Register CORS.
    /// </summary>
    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        app.UseCors(p =>
        {
            p.AllowAnyOrigin();
            // p.WithMethods("GET");
            p.AllowAnyHeader();
        });

        return app;
    }
}