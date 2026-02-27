using Moq;
using recipe.core.DTOs;
using recipe.core.Interfaces;
using recipe.core.Models;
using recipe.core.Services;

public class RecipeServiceTests
{
    private readonly Mock<IRecipeRepository> _mockRepo;
    private readonly RecipeService _service;
    private readonly CreateRecipeDto _recipeDto;

    public RecipeServiceTests()
    {
        _mockRepo = new Mock<IRecipeRepository>();
        _service = new RecipeService(_mockRepo.Object);
        _recipeDto = new CreateRecipeDto
        {
            Name = "Pasta",
            Servings = 4,
            Ingredients = new List<CreateIngredientDto>(),
            Instructions = new List<string> { "Koka pasta" }
        };
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsRecipe()
    {
        var recipe = new Recipe { Id = 1, Name = "Test Recipe" };
        _mockRepo.Setup(r => r.GetByIdAsync(1))
                 .ReturnsAsync(recipe);

        var result = await _service.GetRecipeByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Test Recipe", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(999))
                 .ReturnsAsync((Recipe?)null);

        var result = await _service.GetRecipeByIdAsync(999);  // Fix: 999 inte 1

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateRecipe_ValidRecipe_ReturnsRecept()
    {
        var recipe = new Recipe { Id = 1, Name = "Pasta" };
        _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Recipe>()))  // Fix: r inte recipe (namnkollision)
                 .ReturnsAsync(recipe);

        var result = await _service.CreateRecipeAsync(_recipeDto);  // Fix: använd DTO

        Assert.NotNull(result);
        Assert.Equal("Pasta", result.Name);
    }
    [Fact]
    public async Task GetAllRecipes_ReturnsLista()
    {
        // Arrange
        var recipes = new List<Recipe>
    {
        new Recipe { Id = 1, Name = "Pasta" },
        new Recipe { Id = 2, Name = "Pizza" }
    };
        _mockRepo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(recipes);

        // Act
        var result = await _service.GetAllRecipesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
}