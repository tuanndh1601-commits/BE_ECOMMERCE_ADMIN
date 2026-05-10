using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AutoInjectAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; }
        public AutoInjectAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Lifetime = lifetime;
        }
    }
}
