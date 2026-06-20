using ECOM.APPLICATION.Interfaces.Repositories;
using ECOM.INFRASTRUCTURE.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace ECOM.INFRASTRUCTURE.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAutoDependencies(this IServiceCollection services)
        {
            var applicationAssembly = Assembly.Load("ECOM.APPLICATION");
            var infrastructureAssembly = Assembly.Load("ECOM.INFRASTRUCTURE");

            // 1. Đăng ký Generic Repository mặc định
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            // 2. Tự động Quét Services & Repositories theo Naming Convention nâng cao
            RegisterConventions(services, applicationAssembly, infrastructureAssembly, "Service");
            RegisterConventions(services, applicationAssembly, infrastructureAssembly, "Repository");

            // 3. Đăng ký các Validator tự động từ FluentValidation
            services.AddValidatorsFromAssembly(applicationAssembly);

            return services;
        }

        private static void RegisterConventions(IServiceCollection services, Assembly appAssembly, Assembly infraAssembly, string suffix)
        {
            var interfaces = appAssembly.GetTypes().Where(t => t.IsInterface && t.Name.EndsWith(suffix));
            var implementations = infraAssembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith(suffix)).ToList();

            implementations.AddRange(appAssembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith(suffix)));

            foreach (var @interface in interfaces)
            {
                var implementation = implementations.FirstOrDefault(impl => @interface.IsAssignableFrom(impl));
                if (implementation != null)
                {
                    services.AddScoped(@interface, implementation);
                }
            }
        }
    }
}
