using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Notes.Aplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection service)
        {
            service.AddMediatR(Assembly.GetExecutingAssembly());
            return service;
        }
    }
}
