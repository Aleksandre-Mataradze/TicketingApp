using Application.DTOs.VenueDtos;
using Domain.Models;

namespace Application.Interfaces.IVenueRepository;

public interface IVenueRepository
{
    public Task<bool> AddVenueAsync(Venue venue);
    public Task<Venue> GetVenueAsync(string name);
    public Task<IReadOnlyList<Venue>> GetAllVenueAsync();
    public Task<bool> UpdateVenueAsync(string name, VenueDto venue);
    public Task<bool> DeleteVanueAsync(string name);
}
