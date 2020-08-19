using System;
using Microsoft.Extensions.DependencyInjection;
using VilevePay.Infra.CrossCutting.IoC;

namespace VilevePay.Services.Api.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}