using Application.Common;
using Application.DTOs.VenueDtos;
using Application.Features.VenueRatingFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketingApp.Controllers.venueRatingController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class venueRatingController(VenueRatingFeatures _venueRatingFeatures) : ControllerBase
    {
        [HttpPost]
        public async Task<Result<bool>> addVenueRatingAsync(VenueRatingDto venueRating)
        {
            var result = await _venueRatingFeatures.addVenueRatingAsync(venueRating);
            return result;
        }
        [HttpPut]
        public async Task<Result<bool>> deleteVenueRatingAsync(Guid id)
        {
            var result = await _venueRatingFeatures.deleteVenueRatingAsync(id);
            return result;
        }
    }
}
