//using Bookify.Infrastructure;
//using Bookify.Application;
using Bookify.Infrastructure;
using Bookify.Application;
using Bookify.Api.Extension;
using Bookify.Api.Extensions;
using MediatR;
using Bookify.Application.Apartments.SearchApartments;
using Bookify.Domain.Abstractions;
namespace Bookify.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            //builder.Services.AddOpenApi();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddSwaggerGen();
       

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.ApplyMigrations();
                app.SeedData();

                app.UseSwagger();
                app.UseSwaggerUI();
        

                //app.MapOpenApi(); 
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.MapGet("/apartments", async (
            //                                    ISender sender,
            //                                    DateOnly startDate,
            //                                    DateOnly endDate,
            //                                    CancellationToken cancellationToken) =>
            //{
            //    var query = new SearchApartmentsQuery(startDate, endDate);
            //    Result<IReadOnlyList<ApartmentResponse>> result = await sender.Send(query, cancellationToken);

            //    return Results.Ok(result.Value);
            //});

            //app.MapControllers();

            app.Run();
        }

    }
}
