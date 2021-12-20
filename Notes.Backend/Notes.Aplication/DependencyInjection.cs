using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notes.Aplication.Common.Behaviors;
using System.Reflection;

namespace Notes.Aplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection service)
        {
            service.AddMediatR(Assembly.GetExecutingAssembly());
            service.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            service.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            return service;
        }
    }
}
