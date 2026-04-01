using Application.DTOs;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories;

public class CategoryRepositories(TicketingAppDBContext _dbContext) : ICategoryRepository
{
    public async Task<bool> AddCategoryAsync(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        var result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;
    }

    public async Task<bool> DeleteCategoryAsync(string name)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);

        if (category == null)
        {
            
        }

        category.DeletedAt = DateTime.UtcNow;

        var result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;
    }
    public async Task<Category> GetCategoryAsync(string name)
    {
        var result = await _dbContext.Categories.Where(v => v.DeletedAt == null).FirstOrDefaultAsync(c => c.Name == name);

        if (result == null)
        {
            return null!;
        }
        else
        {
            return result;
        }
    }
    public async Task<IReadOnlyList<Category>> GetAllCategoryAsync()
    {
        var categoryList = await _dbContext.Categories.Where(v => v.DeletedAt == null).ToListAsync();

        return categoryList;
    }

    public async Task<bool> UpdateCategoryAsync(string name, CategoryDto category)
    {
        var existingCategory = await _dbContext.Categories.Where(c => c.DeletedAt == null).FirstOrDefaultAsync(c => c.Name == name);

        existingCategory!.Name = category.name;
        existingCategory.Description = category.description;
        existingCategory.IconUrl = category.iconUrl;

        int result = await _dbContext.SaveChangesAsync();

        return result >= 1 ? true : false;

    }
}