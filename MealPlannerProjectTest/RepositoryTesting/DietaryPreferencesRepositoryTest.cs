using MealPlannerProject.Interfaces;
using MealPlannerProject.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSubstitute;
using System.Data.SqlClient;
using System.Linq;

namespace MealPlannerProjectTest.RepositoryTesting;

[TestClass]
public class DietaryPreferencesRepositoryTest
{
    [TestMethod]
    [System.Obsolete]
    public void AddAlergyAndDietaryPreferenceTest()
    {
        // Arrange
        var mockDataLink = new Mock<IDataLink>();

        // Setup return values for ExecuteScalar
        mockDataLink.Setup(m => m.ExecuteScalar<int>(
            "SELECT dbo.GetUserByName(@u_name)",
            It.IsAny<SqlParameter[]>(),
            false)).Returns(1);

        mockDataLink.Setup(m => m.ExecuteScalar<int>(
            "SELECT dbo.GetDietaryPreferencesByDescription(@dp_description)",
            It.IsAny<SqlParameter[]>(),
            false)).Returns(2);

        mockDataLink.Setup(m => m.ExecuteScalar<int>(
            "SELECT dbo.GetAllergiesByDescription(@a_description)",
            It.IsAny<SqlParameter[]>(),
            false)).Returns(3);

        var repo = new DietaryPreferencesRepository(mockDataLink.Object);

        // Act
        repo.AddAllergyAndDietaryPreference("John", "Doe", "Vegetarian", "Peanuts");

        // Assert the non-query for dietary preference
        mockDataLink.Verify(m => m.ExecuteNonQuery(
            "UpdateUserDietaryPreference",
            It.Is<SqlParameter[]>(ps =>
                ps.Any(p => p.ParameterName == "@u_id" && (int)p.Value == 1) &&
                ps.Any(p => p.ParameterName == "@dp_id" && (int)p.Value == 2)
            )), Times.Once);

        // Assert the non-query for allergies
        mockDataLink.Verify(m => m.ExecuteNonQuery(
            "UpdateUserAllergies",
            It.Is<SqlParameter[]>(ps =>
                ps.Any(p => p.ParameterName == "@u_id" && (int)p.Value == 1) &&
                ps.Any(p => p.ParameterName == "@a_id" && (int)p.Value == 3)
            )), Times.Once);
    }
}
