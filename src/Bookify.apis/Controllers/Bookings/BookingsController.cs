using Bookify.apis.Controllers.Bookings;
using Bookify.Application.Bookings.GetBooking;
using Bookify.Application.Bookings.ReserveBooking;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bookify.Api.Controllers.Bookings
{
    [Route("api/v{version:apiVersion}/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ISender _sender;

        public BookingsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetBookingQuery(id);
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ReserveBooking(ReserveBookingRequest reserveBookingRequest,
                                                        CancellationToken cancellationToken)
        {

            var command = new ReserveBookingCommand(
                reserveBookingRequest.ApartmentId,
                reserveBookingRequest.UserId,
                reserveBookingRequest.StartDate,
            reserveBookingRequest.EndDate);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result);
        }
    }
}
