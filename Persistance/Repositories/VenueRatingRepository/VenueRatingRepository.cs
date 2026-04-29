using Application.DTOs.VenueDtos;
using Application.Interfaces.VenueRatingRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories.VenueRatingRepository;

public class VenueRatingRepository(TicketingAppDBContext _dbContext) : IVenueRatingRepository
{
    public async Task<bool> AddVenueRatingAsync(VenueRating venueRating)
    {
        await _dbContext.VenueRatings.AddAsync(venueRating);

        var result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;
    }

    public async Task<bool> deleteVenueRatingAsync(Guid id)
    {
        var venueRating = await _dbContext.VenueRatings.FirstOrDefaultAsync(v => v.Id == id);

        if (venueRating != null)
        {
            venueRating.DeletedAt = DateTime.Now;

            var result = await _dbContext.SaveChangesAsync();

            return result >= 1 ? true : false;
        }
        else
        {
            return false;
        }
    }
}
