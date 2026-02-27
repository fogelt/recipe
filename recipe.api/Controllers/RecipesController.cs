using Microsoft.AspNetCore.Mvc;
using recipe.core.DTOs;
using recipe.core.Interfaces;
using recipe.core.Models;

namespace recipe.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(IRecipeService recipeService) : ControllerBase
{
  private readonly IRecipeService _recipeService = recipeService;

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Recipe>>> GetAll()
  {
    var recipes = await _recipeService.GetAllRecipesAsync();
    return Ok(recipes);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Recipe>> GetById(int id)
  {
    var recipe = await _recipeService.GetRecipeByIdAsync(id);
    if (recipe == null) return NotFound();
    return Ok(recipe);
  }

  [HttpGet("search")]
  public async Task<ActionResult<IEnumerable<Recipe>>> Search([FromQuery(Name = "q")] string term)
  {
    var results = await _recipeService.SearchRecipesAsync(term);
    return Ok(results);
  }

  [HttpGet("difficulty/{level}")]
  public async Task<ActionResult<IEnumerable<Recipe>>> GetByDifficulty(string level)
  {
    var results = await _recipeService.GetRecipesByDifficultyAsync(level);
    return Ok(results);
  }

  [HttpPost]
  public async Task<ActionResult<Recipe>> Create([FromBody] CreateRecipeDto dto)
  {
    var created = await _recipeService.CreateRecipeAsync(dto);
    return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<Recipe>> Update(int id, [FromBody] CreateRecipeDto dto)
  {
    var updated = await _recipeService.UpdateRecipeAsync(id, dto);
    if (updated == null) return NotFound();
    return Ok(updated);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var success = await _recipeService.DeleteRecipeAsync(id);
    if (!success) return NotFound();
    return NoContent();
  }
}