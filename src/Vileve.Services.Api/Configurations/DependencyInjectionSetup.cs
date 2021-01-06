using System;
using Microsoft.Extensions.DependencyInjection;
using Vileve.Infra.CrossCutting.IoC;

namespace Vileve.Services.Api.Configurations
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