using media_app_api.DTOs;
using media_app_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace media_app_api.Controllers;

[EnableCors("AllowFlutter")]
[ApiController]
[Route("api/[controller]")]
public class ECatalogsController(IECatalogService catalogService) : ControllerBase
{
    // Public GET endpoint for Customer Web & Guests
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? categoryGroupId, [FromQuery] int? categoryId)
    {
        var catalogs = await catalogService.GetECatalogsAsync(categoryGroupId, categoryId);
        return Ok(catalogs);
    }

    // Public GET endpoint for Customer Web & Guests
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var catalog = await catalogService.GetECatalogByIdAsync(id);
        if (catalog is null)
            return NotFound(new { message = $"ECatalog with id {id} not found." });

        return Ok(catalog);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateECatalogDto request)
    {
        var catalog = await catalogService.CreateECatalogAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = catalog.Id }, catalog);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateECatalogDto request)
    {
        var catalog = await catalogService.UpdateECatalogAsync(id, request);
        if (catalog is null)
            return NotFound(new { message = $"ECatalog with id {id} not found." });

        return Ok(catalog);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await catalogService.DeleteECatalogAsync(id);
        if (!success)
            return NotFound(new { message = $"ECatalog with id {id} not found." });

        return NoContent();
    }
}
