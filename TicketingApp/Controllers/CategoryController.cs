using Application.Common;
using Application.DTOs;
using Application.Features.CategoryFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketingApp.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoryController(CategoryFeatures categoryFeatures) : ControllerBase
{
    [HttpPost]
    public async Task<Result<bool>> AddCategoryAsync(CategoryDto category)
    {
        var result = await categoryFeatures.AddCategoryAsync(category);
        return result;
    }

    [HttpGet]
    public async Task<Result<IReadOnlyList<CategoryDto>>> GetAllCategoryAsync()
    {
        var result = await categoryFeatures.GetAllCategoryAsync();

        return result;
    }
    [HttpDelete]
    public async Task<Result<bool>> DeleteCategory(string name)
    {
        var result = await categoryFeatures.DeleteCategoryAsync(name);

        return result;
    }
    [HttpPut]
    public async Task<Result<bool>> UpdateCategory(string name, CategoryDto category)
    {
        var result = await categoryFeatures.UpdateCategoryAsync(name, category);

        return result;
    }
}
