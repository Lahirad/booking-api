using Bookify.apis.Middleware;
using Bookify.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Bookify.apis.Extensions
{
    public static class ApplicationBuilderExtensions
    {

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
           using var scope = app.ApplicationServices.CreateScope();
           using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();   
           dbContext.Database.Migrate();
        }

        public static void UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandlingMiddleware>();    
        }
        public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestContextLoggingMiddleware>();

            return app;
        }
    }
}
