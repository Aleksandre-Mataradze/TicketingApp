using Application.Common;
using Application.DTOs.VenueDtos;
using Application.Interfaces.VenueRatingRepository;
using Domain.Models;

namespace Application.Features.VenueRatingFeatures;

public class VenueRatingFeatures(IVenueRatingRepository _venueRatingRepository)
{
    public async Task<Result<bool>> addVenueRatingAsync(VenueRatingDto venueRating)
    {
        if (venueRating != null)
        {
            var temp = new VenueRating()
            {
                Rating = venueRating.rating,
                Comment = venueRating.comment,
                VenueId = venueRating.venueId,
            };

            var result = await _venueRatingRepository.AddVenueRatingAsync(temp);

            if (result == true)
            {
                return Result<bool>.Ok(result);
            }
            else
            {
                return Result<bool>.Fail("Failed to add venue rating.");
            }

        }else
        {
            return Result<bool>.Fail("Not all fields are filled in.");
        }
    }
    public async Task<Result<bool>> deleteVenueRatingAsync(Guid id)
    {
        var result = await _venueRatingRepository.deleteVenueRatingAsync(id);

        if (result == true)
        {
            return Result<bool>.Ok(result);
        }
        else
        {
            return Result<bool>.Fail("Failed to delete venue rating.");
        }
    }
}
