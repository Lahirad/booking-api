using Bookify.Domain.Apartements;

namespace Bookify.Domain.Bookings;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(
        Apartement apartment,
        DateRange duration,
        CancellationToken cancellationToken = default);

    void Add(Booking booking);
}
