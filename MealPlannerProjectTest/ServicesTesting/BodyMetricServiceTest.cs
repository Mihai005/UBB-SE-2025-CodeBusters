using MealPlannerProject.Interfaces;
using MealPlannerProject.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MealPlannerProjectTest.ServicesTesting
{
    [TestClass]
    public class BodyMetricServiceTests
    {
        private IDataLink _mockDataLink;
        private BodyMetricService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockDataLink = Substitute.For<IDataLink>();
            _service = new BodyMetricService(_mockDataLink);
        }

        [TestMethod]
        [Obsolete]
        public void UpdateUserBodyMetrics_ValidInputs_ExecutesExpectedQueries()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string weight = "75";
            string height = "180";
            string targetGoal = "70";

            int mockUserId = 5;

            // Set up mock responses
            _mockDataLink.ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetUserByName")),
                Arg.Is<SqlParameter[]>(p => p[0].ParameterName == "@u_name" && (string)p[0].Value == "Doe John"),
                Arg.Any<bool>()
            ).Returns(mockUserId);

            // Act
            _service.UpdateUserBodyMetrics(firstName, lastName, weight, height, targetGoal);

            // Assert that ExecuteScalar was called with expected values for the user ID retrieval
            _mockDataLink.Received().ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetUserByName")),
                Arg.Is<SqlParameter[]>(p =>
                    p.Length == 1 &&
                    p[0].ParameterName == "@u_name" &&
                    (string)p[0].Value == "Doe John"
                ),
                false
            );

            // Assert that ExecuteNonQuery was called with the correct parameters to update user body metrics
            _mockDataLink.Received().ExecuteNonQuery(
                Arg.Is<string>(q => q == "UpdateUserBodyMetrics"),
                Arg.Is<SqlParameter[]>(p =>
                    p.Length == 4 &&
                    (int)p[0].Value == mockUserId &&
                    (float)p[1].Value == 75f && // Checking weight
                    (float)p[2].Value == 180f && // Checking height
                    (float)p[3].Value == 70f     // Checking target goal
                )
            );
        }

        [TestMethod]
        [Obsolete]
        public void UpdateUserBodyMetrics_NoTargetGoal_ExecutesExpectedQueriesWithNullGoal()
        {
            // Arrange
            string firstName = "Jane";
            string lastName = "Smith";
            string weight = "65";
            string height = "165";
            string targetGoal = ""; // Empty string to represent no target goal

            int mockUserId = 6;

            // Set up mock responses
            _mockDataLink.ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetUserByName")),
                Arg.Is<SqlParameter[]>(p => p[0].ParameterName == "@u_name" && (string)p[0].Value == "Smith Jane"),
                Arg.Any<bool>()
            ).Returns(mockUserId);

            // Act
            _service.UpdateUserBodyMetrics(firstName, lastName, weight, height, targetGoal);

            // Assert that ExecuteScalar was called with expected values for the user ID retrieval
            _mockDataLink.Received().ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetUserByName")),
                Arg.Is<SqlParameter[]>(p =>
                    p.Length == 1 &&
                    p[0].ParameterName == "@u_name" &&
                    (string)p[0].Value == "Smith Jane"
                ),
                false
            );

            // Assert that ExecuteNonQuery was called with null target goal
            _mockDataLink.Received().ExecuteNonQuery(
                Arg.Is<string>(q => q == "UpdateUserBodyMetrics"),
                Arg.Is<SqlParameter[]>(p =>
                    p.Length == 4 &&
                    (int)p[0].Value == mockUserId &&
                    (float)p[1].Value == 65f && // Checking weight
                    (float)p[2].Value == 165f && // Checking height
                    p[3].Value == DBNull.Value  // Checking that the goal is set to DBNull
                )
            );
        }
    }

}
