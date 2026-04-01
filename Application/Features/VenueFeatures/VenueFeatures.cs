using Application.Common;
using Application.DTOs.VenueDtos;
using Application.Interfaces.IVenueRepository;
using Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.VenueFeatures;

public class VenueFeatures(IVenueRepository _venueRepository)
{
    public async Task<Result<bool>> AddVenueAsync(VenueDto venue)
    {
        if (venue == null)
        {
            return Result<bool>.Fail("Error occured when creating a new event.");
        }
        else
        {
            var tempVenue = new Venue()
            {
                Name = venue.name,
                Address = venue.address,
                Capacity = venue.capacity
            };

            var result = await _venueRepository.AddVenueAsync(tempVenue);

            return Result<bool>.Ok(result);
        }
    }
    public async Task<Result<VenueDto>> GetVenueAsync(string name)
    {
        if (name == null || name == "")
        {
            return Result<VenueDto>.Fail("Please input valid venue name.");
        }
        else
        {
            var result = await _venueRepository.GetVenueAsync(name);

            var tempVenueDto = new VenueDto(result.Name, result.Address, result.Capacity);

            return Result<VenueDto>.Ok(tempVenueDto);
        }
    }
    public async Task<Result<IReadOnlyList<VenueDto>>> GetAllVenueAsync()
    {
        var venueList = await _venueRepository.GetAllVenueAsync();

        if (venueList == null || venueList.Count == 0)
        {
            return Result<IReadOnlyList<VenueDto>>.Fail("An error occured while requesting venue list.");
        }
        else
        {
            var tempVenueList = venueList.Where(v => v.DeletedAt == null).Select(v => new VenueDto(v.Name, v.Address, v.Capacity)).ToList();

            return Result<IReadOnlyList<VenueDto>>.Ok(tempVenueList);
        }
    }
    public async Task<Result<bool>> UpdateVenueAsync(string name, VenueDto venue)
    {
        if (name == null || name == "" || venue == null)
        {
            return Result<bool>.Fail("An error occured while updating the vennue. Please fill all fields correctly");
        }
        else
        {
            var result = await _venueRepository.UpdateVenueAsync(name, venue);

            return Result<bool>.Ok(result);
        }
    }
    public async Task<Result<bool>> DeleteVenueAsync(string name)
    {
        if (name == null || name == "")
        {
            return Result<bool>.Fail("An error occured while deleting venue. Please fill field with valid name.");
        }
        else
        {
            var result = await _venueRepository.DeleteVanueAsync(name);

            return Result<bool>.Ok(result);
        }
    }
}
