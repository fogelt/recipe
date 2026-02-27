using Microsoft.AspNetCore.Mvc;
using Moq;
using recipe.api.Controllers;
using recipe.core.DTOs;
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
        var returnedRecipes = Assert.IsType<IEnumerable<Recipe>>(okResult.Value, exactMatch: false);
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

    [Fact]
    public async Task Create_ReturnsCreatedResult_WithNewRecipe()
    {
        var dto = new CreateRecipeDto { Name = "New Taco", Description = "Taco Desc" };
        var createdRecipe = new Recipe { Id = 10, Name = "New Taco", Description = "Taco Desc" };

        _mockService.Setup(s => s.CreateRecipeAsync(dto))
                    .ReturnsAsync(createdRecipe);

        var result = await _controller.Create(dto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(_controller.GetById), createdResult.ActionName);
        Assert.Equal(10, createdResult.RouteValues?["id"]);

        var returnedRecipe = Assert.IsType<Recipe>(createdResult.Value);
        Assert.Equal(10, returnedRecipe.Id);
    }

    [Fact]
    public async Task Update_ReturnsOk_WhenUpdateIsSuccessful()
    {
        int targetId = 1;
        var dto = new CreateRecipeDto { Name = "Updated Name" };
        var updatedRecipe = new Recipe { Id = targetId, Name = "Updated Name" };

        _mockService.Setup(s => s.UpdateRecipeAsync(targetId, dto))
                    .ReturnsAsync(updatedRecipe);

        var result = await _controller.Update(targetId, dto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var recipe = Assert.IsType<Recipe>(okResult.Value);
        Assert.Equal("Updated Name", recipe.Name);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenDeleted()
    {
        _mockService.Setup(s => s.DeleteRecipeAsync(1)).ReturnsAsync(true);

        var result = await _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }
}