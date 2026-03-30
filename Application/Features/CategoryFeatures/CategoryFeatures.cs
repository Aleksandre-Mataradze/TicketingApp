using Application.DTOs;
using Application.Interfaces;
using Domain.Models;

namespace Application.Features.CategoryFeatures;

public class CategoryFeatures(ICategoryRepository categoryRepository)
{
    public async Task<bool> AddCategoryAsync(CategoryDto category)
    {
        if (category == null)
        {
            return false;
        }
        else
        {
            var temp = new Category()
            {
                Name = category.name,
                Description = category.description,
                IconUrl = category.iconUrl
            };

            bool result = await categoryRepository.AddCategoryAsync(temp);

            return result;
        }
    }
    public async Task<IReadOnlyList<CategoryDto>> GetAllCategoryAsync()
    {
        IReadOnlyList<Category> categories = await categoryRepository.GetAllCategoryAsync();

        if (categories == null || categories.Count == 0)
        {
            return null;
        }
        else
        {
            IReadOnlyList<CategoryDto> categoryDtos = categories.Where(c => c.DeletedAt == null).Select(c => new CategoryDto(c.Name, c.Description, c.IconUrl)
            {
            }).ToList();

            return categoryDtos;
        }
    }
    public async Task<bool> DeleteCategoryAsync(string name)
    {
        if (name == null)
        {
            return false;
        }
        else
        {
            var response = await categoryRepository.DeleteCategoryAsync(name);

            if (response == false)
            {
                return false;
            }
            else
            {
                return response;
            }
        }
    }
    public async Task<bool> UpdateCategoryAsync(string name, CategoryDto category)
    {
        if (name == null || name == "" || category == null)
        {
            return false;
        }
        else
        {
            var result = await categoryRepository.UpdateCategoryAsync(name, category);

            return result;
        }
    }
}