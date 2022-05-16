using Microsoft.Extensions.DependencyInjection;

namespace Communication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommunication(this IServiceCollection services)
        {
            return services;
        }
    }
}