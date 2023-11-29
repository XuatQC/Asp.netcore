namespace FNDSystem
{
    public interface IModule
    {
        IServiceCollection RegisterModule(IServiceCollection services);
        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
    }

    public static class ModuleExtensions
    {
        // ����� DI �R���e�i�ɒǉ����邱�Ƃ��ł��܂�
        static readonly List<IModule> registeredModules = new List<IModule>();

        public static IServiceCollection RegisterModules(this IServiceCollection services)
        {
            var modules = DiscoverModules();
            foreach (var module in modules)
            {
                module.RegisterModule(services);
                registeredModules.Add(module);
            }

            return services;
        }

        public static WebApplication MapEndpoints(this WebApplication app)
        {
            foreach (var module in registeredModules)
            {
                module.MapEndpoints(app.MapGroup("/api"));
            }
            return app;
        }

        private static IEnumerable<IModule> DiscoverModules()
        {
            return typeof(IModule).Assembly
                .GetTypes()
                .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
                .Select(Activator.CreateInstance)
                .Cast<IModule>();
        }

    }
}