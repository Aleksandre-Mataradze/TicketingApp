using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface ICategoryRepository
{
    public Task<bool> AddCategoryAsync(Category category);
    public Task<IReadOnlyList<Category>> GetAllCategoryAsync();
    public Task<bool> UpdateCategoryAsync(string name, CategoryDto category);
    public Task<bool> DeleteCategoryAsync(string name);
}