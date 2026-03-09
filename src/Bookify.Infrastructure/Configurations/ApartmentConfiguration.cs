using Bookify.Domain.Apartements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Configurations
{
    internal sealed class ApartmentConfiguration : IEntityTypeConfiguration<Apartement>
    {
        public void Configure(EntityTypeBuilder<Apartement> builder)
        {
            builder.ToTable("apartments");

            builder.HasKey(apartment => apartment.Id);

            builder.OwnsOne(apartment => apartment.Address);

            builder.Property(apartment => apartment.Name)
                .HasMaxLength(200)
                .HasConversion(name => name.value, value => new Name(value));

            builder.Property(apartment => apartment.Description)
                .HasMaxLength(2000)
                .HasConversion(description => description.value, value => new Description(value));

            builder.OwnsOne(apartment => apartment.Price, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });

            builder.OwnsOne(apartment => apartment.CleaningFee, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });

            builder.Property<uint>("Version").IsRowVersion();
        }
    }
}
