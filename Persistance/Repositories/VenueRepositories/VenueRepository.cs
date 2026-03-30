using Application.DTOs.VenueDtos;
using Application.Interfaces.IVenueRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories.VenueRepositories;

public class VenueRepository(TicketingAppDBContext _dbContext) : IVenueRepository
{
    public async Task<bool> AddVenueAsync(Venue venue)
    {
        await _dbContext.Venues.AddAsync(venue);
        var result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;
    }

    public async Task<bool> DeleteVanueAsync(string name)
    {
        var venue = await _dbContext.Venues.FirstOrDefaultAsync(v => v.Name == name);

        if (venue == null)
        {
            return false;
        }
        else
        {
            venue!.DeletedAt = DateTime.UtcNow;

            

            var result = await _dbContext.SaveChangesAsync();

            return result >= 1 ? true : false;
        }
    }

    public async Task<IReadOnlyList<Venue>> GetAllVenueAsync()
    {
        var venueList = await _dbContext.Venues.ToListAsync();

        return venueList;
    }

    public async Task<Venue> GetVenueAsync(string name)
    {
        var venue = await _dbContext.Venues.Where(v => v.DeletedAt == null).FirstOrDefaultAsync(v => v.Name == name);

        return venue!;
    }

    public async Task<bool> UpdateVenueAsync(string name, VenueDto venue)
    {
        var existingVenue = await _dbContext.Venues.Where(v => v.DeletedAt == null).FirstOrDefaultAsync(v => v.Name == name);

        existingVenue!.Name = venue.name;
        existingVenue.Address = venue.address;
        existingVenue.Capacity = venue.capacity;

        int result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;
    }
}
