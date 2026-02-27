using recipe.core.Models;
using recipe.core.services;
using recipe.core.Interfaces;
using Moq;
public class RecipeServiceTests
{
    private readonly Mock<IGenericRepository<Recipe>> _mockRepo;
    private readonly RecipeService _service;

    public RecipeServiceTests()
    {
        _mockRepo = new Mock<IGenericRepository<Recipe>>();
        _service = new RecipeService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsRecipe()
    {
        // Arrange
        var recipe = new Recipe { Id = 1, Name = "Test Recipe" };
        _mockRepo.Setup(r => r.GetByIdAsync(1))
                 .ReturnsAsync(recipe);

        // Act
        var result = await _service.GetRecipeByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Recipe", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetByIdAsync(999))
                 .ReturnsAsync((Recipe?)null);

        // Act
        var result = await _service.GetRecipeByIdAsync(1);

        // Assert
        Assert.Null(result);
    }
}