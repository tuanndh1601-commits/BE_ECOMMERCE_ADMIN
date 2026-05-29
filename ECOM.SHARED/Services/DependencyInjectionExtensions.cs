using ECOM.SHARED.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECOM.SHARED.Services
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAutoDI(this IServiceCollection services, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(x => x.GetTypes()).Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType).ToList();
            foreach (var implementationType in types)
            {
                var interfaces = implementationType
                    .GetInterfaces()
                    .Where(i => i != typeof(IScopeDependency)
                             && i != typeof(ITransientDependency)
                             && i != typeof(ISingletonDependency))
                    .ToList();

                if (typeof(IScopeDependency).IsAssignableFrom(implementationType))
                {
                    foreach (var serviceType in interfaces)
                    {
                        services.AddScoped(serviceType, implementationType);
                    }
                }
                else if (typeof(ITransientDependency).IsAssignableFrom(implementationType))
                {
                    foreach (var serviceType in interfaces)
                    {
                        services.AddTransient(serviceType, implementationType);
                    }
                }
                else if (typeof(ISingletonDependency).IsAssignableFrom(implementationType))
                {
                    foreach (var serviceType in interfaces)
                    {
                        services.AddSingleton(serviceType, implementationType);
                    }
                }
            }

            return services;
        }
    }
}
