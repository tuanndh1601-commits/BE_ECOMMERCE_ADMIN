using ECOM.INFRASTRUCTURE.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.Scan(scan => scan
                // Quét tất cả các Assembly bắt đầu bằng "ECOM"
                .FromApplicationDependencies(a => a.FullName.StartsWith("ECOM"))

                // Đăng ký cho Repositories
                .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()

                // Đăng ký cho Services (Tầng Application)
                .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );

            return services;
        }
    }
}
