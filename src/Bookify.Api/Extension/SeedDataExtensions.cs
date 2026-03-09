using System.Data;
using Bogus;
using Bookify.Application.Abstractions.Data;
using Bookify.Domain.Apartements;
using Bookify.Domain.Apartments;
using Dapper;

namespace Bookify.Api.Extensions;

internal static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();

        var apartments = new List<object>();
        for (int i = 0; i < 100; i++)
        {
            apartments.Add(new
            {
                Id = Guid.NewGuid(),
                Name = faker.Company.CompanyName(),
                Description = "Amazing view",
                Address_Country = faker.Address.Country(),
                Address_State = faker.Address.State(),
                Address_ZipCode = faker.Address.ZipCode(),
                Address_City = faker.Address.City(),
                Address_Street = faker.Address.StreetAddress(),
                Price_Amount = faker.Random.Decimal(50, 1000),
                Price_Currency = "USD",
                CleaningFee_Amount = faker.Random.Decimal(25, 200),
                CleaningFee_Currency = "USD",
                Amenities = "1,2",
                LastBookedOnUtc = (DateTime?)null,
                Version = 1L
            });
        }

        const string sql = """
        INSERT INTO dbo.apartments
        (
            Id,
            Name,
            Description,
            Address_Country,
            Address_State,
            Address_ZipCode,
            Address_City,
            Address_Street,
            Price_Amount,
            Price_Currency,
            CleaningFee_Amount,
            CleaningFee_Currency,
            Amenities,
            LastBookedOnUtc,
            Version
        )
        VALUES
        (
            @Id,
            @Name,
            @Description,
            @Address_Country,
            @Address_State,
            @Address_ZipCode,
            @Address_City,
            @Address_Street,
            @Price_Amount,
            @Price_Currency,
            @CleaningFee_Amount,
            @CleaningFee_Currency,
            @Amenities,
            @LastBookedOnUtc,
            @Version
        );
        """;

        connection.Execute(sql, apartments);
    }
}
