using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Models;

namespace Application.Features.CategoryFeatures;

public class CategoryFeatures(ICategoryRepository categoryRepository)
{
    public async Task<Result<bool>> AddCategoryAsync(CategoryDto category)
    {
        if (category == null)
        {
            return Result<bool>.Fail("Error occured when creating a new category.");
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

            return Result<bool>.Ok(result);
        }
    }
    public async Task<Result<IReadOnlyList<CategoryDto>>> GetAllCategoryAsync()
    {
        IReadOnlyList<Category> categories = await categoryRepository.GetAllCategoryAsync();

        if (categories == null || categories.Count == 0)
        {
            return Result<IReadOnlyList<CategoryDto>>.Fail("An error occured when requesting category list.");
        }
        else
        {
            var categoryDtos = categories.Where(c => c.DeletedAt == null).Select(c => new CategoryDto(c.Name, c.Description, c.IconUrl)
            {
            }).ToList();

            return Result<IReadOnlyList<CategoryDto>>.Ok(categoryDtos);
        }
    }
    public async Task<Result<bool>> DeleteCategoryAsync(string name)
    {
        if (name == null)
        {
            return Result<bool>.Fail("Input value was not valid.");
        }
        else
        {
            var response = await categoryRepository.DeleteCategoryAsync(name);

            if (response == false)
            {
                return Result<bool>.Fail("An error occured while deleting category");
            }
            else
            {
                return Result<bool>.Ok(response);
            }
        }
    }
    public async Task<Result<bool>> UpdateCategoryAsync(string name, CategoryDto category)
    {
        if (name == null || name == "" || category == null)
        {
            return Result<bool>.Fail("Category name or new category information was not filled correctly!");
        }
        else
        {
            var result = await categoryRepository.UpdateCategoryAsync(name, category);

            return Result<bool>.Ok(result);
        }
    }
}