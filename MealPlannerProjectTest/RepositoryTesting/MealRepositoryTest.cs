namespace MealPlannerProjectTest.RepositoryTesting
{
    using MealPlannerProject.Models;
    using MealPlannerProject.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System.Data.SqlClient;
    using System.Linq;
    using MealPlannerProject.Interfaces;
    using System.Threading.Tasks;

    [TestClass]
    public class MealRepositoryTests
    {
        [TestMethod]
        [System.Obsolete]
        public async Task CreateMealAsync_ShouldCallExecuteScalarWithCorrectParameters()
        {
            // Arrange
            var dataLink = Substitute.For<IDataLink>();
            var repository = new MealRepository(dataLink);
            var meal = new Meal
            {
                Name = "Chicken Salad",
                Recipe = "Mix everything.",
                PreparationTime = 15,
                Servings = 2,
                Protein = 20,
                Calories = 300,
                Carbohydrates = 10,
                Fat = 12,
                Fiber = 3,
                Sugar = 2,
                PhotoLink = "photo.jpg"
            };

            dataLink.ExecuteScalar<int>(Arg.Any<string>(), Arg.Any<SqlParameter[]>(), false)
                .Returns(10);

            // Act
            var result = await repository.CreateMealAsync(meal, 1, 2);

            // Assert
            Assert.AreEqual(10, result);
            dataLink.Received().ExecuteScalar<int>(
                Arg.Any<string>(),
                Arg.Is<SqlParameter[]>(p =>
                    p.Any(x => x.ParameterName == "@m_name" && (string)x.Value == "Chicken Salad") &&
                    p.Any(x => x.ParameterName == "@cs_id" && (int)x.Value == 1) &&
                    p.Any(x => x.ParameterName == "@mt_id" && (int)x.Value == 2)
                ),
                false
            );
        }

        [TestMethod]
        [System.Obsolete]
        public async Task AddMealIngredientAsync_ShouldCallExecuteNonQueryWithCorrectParameters()
        {
            // Arrange
            var dataLink = Substitute.For<IDataLink>();
            var repository = new MealRepository(dataLink);

            dataLink.ExecuteNonQuery("InsertMealIngredient", Arg.Any<SqlParameter[]>())
                .Returns(1);

            // Act
            var result = await repository.AddMealIngredientAsync(5, 8, 2.5f);

            // Assert
            Assert.AreEqual(1, result);
            dataLink.Received().ExecuteNonQuery(
                "InsertMealIngredient",
                Arg.Is<SqlParameter[]>(p =>
                    p.Any(x => x.ParameterName == "@mealId" && (int)x.Value == 5) &&
                    p.Any(x => x.ParameterName == "@ingredientId" && (int)x.Value == 8) &&
                    p.Any(x => x.ParameterName == "@quantity" && (float)x.Value == 2.5f)
                )
            );
        }
    }
}
