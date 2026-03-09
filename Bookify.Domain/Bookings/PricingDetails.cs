using Bookify.Domain.Apartements;
namespace Bookify.Domain.Bookings;
public sealed record PricingDetails(
    Money PriceForPeriod,
    Money CleaningFee,
    Money AmenitiesUpCharge,
    Money TotalPrice);
