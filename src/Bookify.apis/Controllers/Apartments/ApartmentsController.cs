using Bookify.Application.Apartments.SearchApartments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.apis.Controllers.Apartments
{
    //[Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/apartments")]
   
    public class ApartmentsController : ControllerBase
    {
        private readonly ISender _sender;

        public ApartmentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> SearchApartments(   DateOnly starDate
                                                            ,DateOnly endDate
                                                            ,CancellationToken cancellationToken)
        {
            
            var query = new  SearchApartmentsQuery(starDate, endDate);

             var result = await _sender.Send(query, cancellationToken); 
            return Ok(result);
        }   
    }
}
