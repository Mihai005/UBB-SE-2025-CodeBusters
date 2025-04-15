namespace MealPlannerProjectTest.ServiceTesting
{

    using MealPlannerProject.Queries;
    using MealPlannerProject.Services;
    using MealPlannerProject.Exceptions;
    using NSubstitute;
    using System.Data.SqlClient;
    using Xunit;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
    using MealPlannerProject.Interfaces;

    [TestClass]
    public class GoalPageServiceTests
    {
        [TestMethod]
        [System.Obsolete]
        public void AddGoals_ShouldCallCorrectQueries()
        {
            // Arrange
            var dataLink = Substitute.For<IDataLink>();
            var service = new GoalPageService(dataLink);

            // Mock ExecuteScalar to return valid user and goal IDs
            dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", Arg.Any<SqlParameter[]>(), false)
                .Returns(1);
            dataLink.ExecuteScalar<int>("SELECT dbo.GetGoalByDescription(@g_description)", Arg.Any<SqlParameter[]>(), false)
                .Returns(2);

            // Act
            service.AddGoals("John", "Doe", "Lose weight");

            // Assert
            dataLink.Received().ExecuteNonQuery(
                "UpdateUserGoals",
                Arg.Is<SqlParameter[]>(p =>
                    p.Any(x => x.ParameterName == "@u_id" && (int)x.Value == 1) &&
                    p.Any(x => x.ParameterName == "@g_id" && (int)x.Value == 2)
                )
            );
        }

        [TestMethod]
        [System.Obsolete]
        public void AddGoals_ShouldHandleUserNotFound()
        {
            // Arrange
            var dataLink = Substitute.For<IDataLink>();
            var service = new GoalPageService(dataLink);

            // Mock ExecuteScalar to return a null (no user found)
            dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", Arg.Any<SqlParameter[]>(), false)
                .Returns(0); // Assume 0 indicates no user found

            // Act & Assert
            Assert.Throws<DatabaseOperationException>(() => service.AddGoals("John", "Doe", "Lose weight"));
        }

        [TestMethod]
        [System.Obsolete]
        public void AddGoals_ShouldHandleGoalNotFound()
        {
            // Arrange
            var dataLink = Substitute.For<IDataLink>();
            var service = new GoalPageService(dataLink);

            // Mock ExecuteScalar to return valid user ID but no goal found
            dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", Arg.Any<SqlParameter[]>(), false)
                .Returns(1);
            dataLink.ExecuteScalar<int>("SELECT dbo.GetGoalByDescription(@g_description)", Arg.Any<SqlParameter[]>(), false)
                .Returns(0); // Assume 0 indicates no goal found

            // Act & Assert
            Assert.Throws<DatabaseOperationException>(() => service.AddGoals("John", "Doe", "Lose weight"));
        }

        [TestMethod]
        [System.Obsolete]
        public void AddGoals_ShouldCallExecuteNonQuery_WhenValidParameters()
        {
            // Arrange
            var dataLink = Substitute.For<IDataLink>();
            var service = new GoalPageService(dataLink);

            // Mock ExecuteScalar to return valid user and goal IDs
            dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", Arg.Any<SqlParameter[]>(), false)
                .Returns(1);
            dataLink.ExecuteScalar<int>("SELECT dbo.GetGoalByDescription(@g_description)", Arg.Any<SqlParameter[]>(), false)
                .Returns(2);

            // Act
            service.AddGoals("John", "Doe", "Lose weight");

            // Assert
            dataLink.Received().ExecuteNonQuery(
                "UpdateUserGoals",
                Arg.Is<SqlParameter[]>(p =>
                    p.Any(x => x.ParameterName == "@u_id" && (int)x.Value == 1) &&
                    p.Any(x => x.ParameterName == "@g_id" && (int)x.Value == 2)
                )
            );
        }
    }
}