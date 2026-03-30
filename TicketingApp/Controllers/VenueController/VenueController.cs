using Application.DTOs.VenueDtos;
using Application.Features.VenueFeatures;
using Microsoft.AspNetCore.Mvc;

namespace TicketingApp.Controllers.VenueController;

[ApiController]
[Route("api/[controller]")]
public class VenueController(VenueFeatures _venueFeatures) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<bool>> AddVenueAsync(VenueDto venue)
    {
        var result = await _venueFeatures.AddVenueAsync(venue);

        return Ok(result);
    }
    [HttpGet("{name}")]
    public async Task<ActionResult<VenueDto>> GetVenueAsync(string name)
    {
        var result = await _venueFeatures.GetVenueAsync(name);

        return Ok(result);
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<VenueDto>>> GetAllVenueAsync()
    {
        var result = await _venueFeatures.GetAllVenueAsync();

        return Ok(result);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateVenueAsync(string name, VenueDto venue)
    {
        var result = await _venueFeatures.UpdateVenueAsync(name, venue);

        return Ok(result);
    }
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteVenueAsync(string name)
    {
        var result = await _venueFeatures.DeleteVenueAsync(name);
        
        return Ok(result);
    }
}
