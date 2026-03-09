using Bookify.Application.Bookings.GetBooking;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Bookings
{
    [Route("api/bookings")]
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

            //if (result.IsFailure)
            //    return NotFound(result.Error); // or BadRequest depending on your error

            return Ok(result);
        }
    }
}
