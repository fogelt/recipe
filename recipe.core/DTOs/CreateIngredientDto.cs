using System.ComponentModel.DataAnnotations;

namespace recipe.core.DTOs;

public class CreateIngredientDto
{
  [Required]
  public int Id { get; set; }
  [Required]
  [StringLength(100, MinimumLength = 3)]
  public string? Name { get; set; }
  [Required]
  public decimal Quantity { get; set; }
  [Required]
  public string? Unit { get; set; }
}