using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Communication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommunication(this IServiceCollection services)
        {
            return services;
        }
    }
}