using media_app_api.DTOs;
using media_app_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace media_app_api.Controllers;

[EnableCors("AllowFlutter")]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    // Public GET endpoint for Customer Web & Guests
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await categoryService.GetCategoriesAsync();
        return Ok(categories);
    }

    // Public GET endpoint for Customer Web & Guests
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await categoryService.GetCategoryByIdAsync(id);
        if (category is null)
            return NotFound(new { message = $"Category with id {id} not found." });

        return Ok(category);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto request)
    {
        var category = await categoryService.CreateCategoryAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto request)
    {
        var category = await categoryService.UpdateCategoryAsync(id, request);
        if (category is null)
            return NotFound(new { message = $"Category with id {id} not found." });

        return Ok(category);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await categoryService.DeleteCategoryAsync(id);
        if (!success)
            return NotFound(new { message = $"Category with id {id} not found." });

        return NoContent();
    }
}
