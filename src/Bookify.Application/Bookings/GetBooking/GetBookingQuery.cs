using Bookify.Application.Abstractions.Caching;
using Bookify.Application.Abstractions.Messging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Bookings.GetBooking
{
    public record GetBookingQuery(Guid BookingId) : ICachedQuery<BookingResponse>
    {
        public string CacheKey => $"bookings-{BookingId}";
        public TimeSpan? Expiration => null;
    }
}
