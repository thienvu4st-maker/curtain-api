using media_app_api.DTOs;
using media_app_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace media_app_api.Controllers;

[EnableCors("AllowFlutter")]
[ApiController]
[Route("api/[controller]")]
public class CategoryGroupsController(ICategoryGroupService groupService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var groups = await groupService.GetCategoryGroupsAsync();
        return Ok(groups);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var group = await groupService.GetCategoryGroupByIdAsync(id);
        if (group is null)
            return NotFound(new { message = $"Category group with id {id} not found." });

        return Ok(group);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryGroupDto request)
    {
        var group = await groupService.CreateCategoryGroupAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryGroupDto request)
    {
        var group = await groupService.UpdateCategoryGroupAsync(id, request);
        if (group is null)
            return NotFound(new { message = $"Category group with id {id} not found." });

        return Ok(group);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await groupService.DeleteCategoryGroupAsync(id);
        if (!success)
            return NotFound(new { message = $"Category group with id {id} not found." });

        return NoContent();
    }
}
