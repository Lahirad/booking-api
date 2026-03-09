using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Bookings;
using Bookify.Application.Abstractions.Behaviors;
using FluentValidation;

namespace Bookify.Application
{
    public static class DependencyInjuction
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjuction).Assembly);
                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
                configuration.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjuction).Assembly, includeInternalTypes: true);

            services.AddTransient<PricingService>();
            return services;
        }
    }
}
