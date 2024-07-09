using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Admin.Core.Entity.Masters;
using Admin.Application.Validators.Master;

namespace Admin.Application.Extentions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<State>, CreateStateValidator>();

            return services;
        }
    }
}
