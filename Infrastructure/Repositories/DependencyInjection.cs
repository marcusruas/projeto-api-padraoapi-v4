using Microsoft.Extensions.DependencyInjection;
using Repositories.Area.Repository;

namespace Infrastructure.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAreaRepository, AreaRepository>();
            return services;
        }
    }
}