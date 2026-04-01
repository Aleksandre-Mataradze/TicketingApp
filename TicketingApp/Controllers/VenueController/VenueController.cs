using Application.Common;
using Application.DTOs.VenueDtos;
using Application.Features.VenueFeatures;
using Microsoft.AspNetCore.Mvc;

namespace TicketingApp.Controllers.VenueController;

[ApiController]
[Route("api/[controller]")]
public class VenueController(VenueFeatures _venueFeatures) : ControllerBase
{
    [HttpPost]
    public async Task<Result<bool>> AddVenueAsync(VenueDto venue)
    {
        var result = await _venueFeatures.AddVenueAsync(venue);

        return result;
    }
    [HttpGet("{name}")]
    public async Task<Result<VenueDto>> GetVenueAsync(string name)
    {
        var result = await _venueFeatures.GetVenueAsync(name);

        return result;
    }
    [HttpGet]
    public async Task<Result<IReadOnlyList<VenueDto>>> GetAllVenueAsync()
    {
        var result = await _venueFeatures.GetAllVenueAsync();

        return result;
    }
    [HttpPut]
    public async Task<Result<bool>> UpdateVenueAsync(string name, VenueDto venue)
    {
        var result = await _venueFeatures.UpdateVenueAsync(name, venue);

        return result;
    }
    [HttpDelete]
    public async Task<Result<bool>> DeleteVenueAsync(string name)
    {
        var result = await _venueFeatures.DeleteVenueAsync(name);
        
        return result;
    }
}
