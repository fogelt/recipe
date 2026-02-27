using Microsoft.AspNetCore.Mvc;
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
    return NoContent();
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Recipe>> GetById(int id)
  {
    return NoContent();
  }

  [HttpGet("search")]
  public async Task<ActionResult<IEnumerable<Recipe>>> Search([FromQuery] string q)
  {
    return NoContent();
  }

  [HttpPost]
  public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipe)
  {
    return NoContent();
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] Recipe recipe)
  {
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    return NoContent();
  }

  [HttpGet("difficulty/{level}")]
  public async Task<ActionResult<IEnumerable<Recipe>>> GetByDifficulty(string level)
  {
    return NoContent();
  }
}