using Microsoft.AspNetCore.Mvc;
using Moq;
using recipe.api.Controllers;
using recipe.core.Interfaces;
using recipe.core.Models;
namespace recipe.tests.ControllerTests;

public class RecipesControllerTests
{
    private readonly Mock<IRecipeService> _mockService;
    private readonly RecipesController _controller;

    public RecipesControllerTests()
    {
        _mockService = new Mock<IRecipeService>();
        _controller = new RecipesController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfRecipes()
    {
        // Arrange 
        var mockRecipes = new List<Recipe>
        {
            new Recipe { Id = 1, Name = "Pancakes" },
            new Recipe { Id = 2, Name = "Waffles" }
        };
        _mockService.Setup(s => s.GetAllRecipesAsync()).ReturnsAsync(mockRecipes);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRecipes = Assert.IsAssignableFrom<IEnumerable<Recipe>>(okResult.Value);
        Assert.Equal(2, returnedRecipes.Count());
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenRecipeDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetRecipeByIdAsync(99)).ReturnsAsync((Recipe?)null);

        // Act
        var result = await _controller.GetById(99);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenRecipeExists()
    {
        // Arrange
        var mockRecipe = new Recipe { Id = 1, Name = "Pancakes" };
        _mockService.Setup(s => s.GetRecipeByIdAsync(1)).ReturnsAsync(mockRecipe);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var recipe = Assert.IsType<Recipe>(okResult.Value);
        Assert.Equal("Pancakes", recipe.Name);
    }
}