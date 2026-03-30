using Application.DTOs.VenueDtos;
using Application.Interfaces.IVenueRepository;
using Domain.Models;

namespace Application.Features.VenueFeatures;

public class VenueFeatures(IVenueRepository _venueRepository)
{
    public async Task<bool> AddVenueAsync(VenueDto venue)
    {
        if (venue == null)
        {
            return false;
        }
        else
        {
            var tempVenue = new Venue()
            {
                Name = venue.name,
                Address = venue.address,
                Capacity = venue.capacity
            };

            return await _venueRepository.AddVenueAsync(tempVenue);
        }
    }
    public async Task<VenueDto> GetVenueAsync(string name)
    {
        if (name == null || name == "")
        {
            return null!;
        }
        else
        {
            var result = await _venueRepository.GetVenueAsync(name);

            var tempVenueDto = new VenueDto(result.Name, result.Address, result.Capacity);

            return tempVenueDto;
        }
    }
    public async Task<IReadOnlyList<VenueDto>> GetAllVenueAsync()
    {
        var venueList = await _venueRepository.GetAllVenueAsync();

        if (venueList == null || venueList.Count == 0)
        {
            return null!;
        }
        else
        {
            var tempVenueList = venueList.Where(v => v.DeletedAt == null).Select(v => new VenueDto(v.Name, v.Address, v.Capacity)).ToList();

            return tempVenueList;
        }
    }
    public async Task<bool> UpdateVenueAsync(string name, VenueDto venue)
    {
        if (name == null || name == "" || venue == null)
        {
            return false;
        }
        else
        {
            return await _venueRepository.UpdateVenueAsync(name, venue);
        }
    }
    public async Task<bool> DeleteVenueAsync(string name)
    {
        if (name == null || name == "")
        {
            return false;
        }
        else
        {
            return await _venueRepository.DeleteVanueAsync(name);
        }
    }
}
