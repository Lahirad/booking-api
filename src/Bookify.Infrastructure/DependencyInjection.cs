using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Caching;
using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartements;
using Bookify.Domain.Bookings;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Authentication;
using Bookify.Infrastructure.Authorization;
using Bookify.Infrastructure.Caching;
using Bookify.Infrastructure.Clock;
using Bookify.Infrastructure.Data;
using Bookify.Infrastructure.Email;
using Bookify.Infrastructure.Outbox;
using Bookify.Infrastructure.Repositories;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure
{
    public static class DependencyInjuction
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IEmailService, EmailService>();

            AddPersistence(services, configuration);
            AddAuthentication(services, configuration);
            AddAuthorization(services);
            AddCaching(services, configuration);
            AddHealthChecks(services, configuration);
            AddBackgroundJobs(services, configuration); 
            return services;
        }

        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                           .AddJwtBearer();//this line will read the header token and set the claim as authencated successfully.

            services.Configure<Authentication.AuthenticationOptions>(configuration.GetSection("Authentication"));

            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

            services.AddTransient<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<Application.Abstractions.Authentication.IAuthenticationService, Authentication.AuthenticationService>((serviceProvider, httpClient) =>
            {
                KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
            {
                KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
            });
            services.AddHttpContextAccessor();

            services.AddScoped<IUserContext, UserContext>();
        }

        private static void AddAuthorization(IServiceCollection services)
        {
            services.AddScoped<AuthorizationService>();
            services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();
            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        }
        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database")
                                    ?? throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDBContext>(option =>
            {
                option
                .UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApartementRepository, ApartmentRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IUnitofWork>(sp => sp.GetRequiredService<ApplicationDBContext>());

            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString)); // Dapper connection factory

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        }

        private static void AddCaching(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Cache") ??
                                      throw new ArgumentNullException(nameof(configuration));

            services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

            services.AddSingleton<ICacheService, CacheService>();
        }
        private static void AddHealthChecks(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("Database")!)
                .AddRedis(configuration.GetConnectionString("Cache")!)
                .AddUrlGroup(new Uri(configuration["KeyCloak:BaseUrl"]!), HttpMethod.Get, "keycloak");
        }

        private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

            services.AddQuartz();

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
        }
    }
}
