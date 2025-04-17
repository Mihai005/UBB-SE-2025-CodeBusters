using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using Moq;
using Xunit;
using MealPlannerProject.Interfaces;
using MealPlannerProject.Repositories;
using System.Threading.Tasks;

namespace MealPlannerProjectTest.RepositoryTesting
{
    [TestClass]
    public class IngredientRepositoryTests
    {

        [TestMethod]
        public async Task GetIngredientByNameAsync_ShouldReturnIngredient_WhenIngredientExists()
        {
            var _mockDataLink = new Mock<IDataLink>();

            // Inject mock DataLink into IngredientRepository
            var _ingredientRepository = new IngredientRepository(_mockDataLink.Object);
            // Arrange
            var ingredientName = "Tomato";
            var mockTable = new DataTable();
            mockTable.Columns.Add("i_id", typeof(int));
            mockTable.Columns.Add("i_name", typeof(string));
            mockTable.Columns.Add("calories", typeof(float));
            mockTable.Columns.Add("protein", typeof(float));
            mockTable.Columns.Add("carbohydrates", typeof(float));
            mockTable.Columns.Add("fat", typeof(float));
            mockTable.Columns.Add("fiber", typeof(float));
            mockTable.Columns.Add("sugar", typeof(float));

            mockTable.Rows.Add(1, "Tomato", 22.5f, 1.1f, 4.5f, 0.2f, 3.0f, 3.6f);

            _mockDataLink.Setup(dl => dl.ExecuteSqlQuery(It.IsAny<string>(), It.IsAny<System.Data.SqlClient.SqlParameter[]?>())) 
                         .Returns(mockTable);

            // Act
            var ingredient = await _ingredientRepository.GetIngredientByNameAsync(ingredientName);

            // Assert
            Xunit.Assert.NotNull(ingredient);
            Xunit.Assert.Equal("Tomato", ingredient.Name);
            Xunit.Assert.Equal(1, ingredient.Id);
            Xunit.Assert.Equal(22.5f, ingredient.Calories);
        }

        [TestMethod]
        public async Task GetIngredientByNameAsync_ShouldReturnNull_WhenIngredientDoesNotExist()
        {
            var _mockDataLink = new Mock<IDataLink>();

            // Inject mock DataLink into IngredientRepository
            var _ingredientRepository = new IngredientRepository(_mockDataLink.Object);
            // Arrange
            var ingredientName = "NonExistentIngredient";
            var mockTable = new DataTable();

            _mockDataLink.Setup(dl => dl.ExecuteSqlQuery(It.IsAny<string>(), It.IsAny<System.Data.SqlClient.SqlParameter[]?>())) 
                         .Returns(mockTable);

            // Act
            var ingredient = await _ingredientRepository.GetIngredientByNameAsync(ingredientName);

            // Assert
            Xunit.Assert.Null(ingredient);
        }
    }
}
