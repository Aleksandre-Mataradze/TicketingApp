using Application.DTOs.VenueDtos;
using Domain.Models;

namespace Application.Interfaces.VenueRatingRepository;

public interface IVenueRatingRepository
{
    public Task<bool> AddVenueRatingAsync(VenueRating venueRatingDto);
    public Task<bool> deleteVenueRatingAsync(Guid id);
}

