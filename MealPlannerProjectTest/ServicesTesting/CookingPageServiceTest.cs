using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Repositories;
using MealPlannerProject.Interfaces.Repositories;
using MealPlannerProject.Interfaces;
using MealPlannerProject.Interfaces.Services;
using MealPlannerProject.Services;
using NSubstitute;
using System.Data.SqlClient;
using System;
using NSubstitute.ExceptionExtensions;

namespace MealPlannerProjectTest.ServicesTesting
{
    [TestClass]
    public class CookingPageServiceTest
    {
        private IDataLink _mockDataLink;
        private ICookingPageService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockDataLink = Substitute.For<IDataLink>();
            _service = new CookingPageService();
        }

        [TestMethod]
        [Obsolete]
        public void AddCookingSkill_ValidInputs_ExecutesExpectedQueries()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string cookingDesc = "Grilling";

            int mockUserId = 5;
            int mockCookingSkillId = 10;

            // Set up mock responses
            _mockDataLink.ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetUserByName")),
                Arg.Is<SqlParameter[]>(p => p[0].ParameterName == "@u_name" && (string)p[0].Value == "Doe John"),
                Arg.Any<bool>()
            ).Returns(mockUserId);

            _mockDataLink.ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetCookingSkillByDescription")),
                Arg.Is<SqlParameter[]>(p => p[0].ParameterName == "@cs_description" && (string)p[0].Value == cookingDesc),
                Arg.Any<bool>()
            ).Returns(mockCookingSkillId);

            // Act
            _service.AddCookingSkill(firstName, lastName, cookingDesc);

            // Assert that ExecuteScalar was called with expected values
            _mockDataLink.Received().ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetUserByName")),
                Arg.Is<SqlParameter[]>(p =>
                    p.Length == 1 &&
                    p[0].ParameterName == "@u_name" &&
                    (string)p[0].Value == "Doe John"
                ),
                false
            );

            _mockDataLink.Received().ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetCookingSkillByDescription")),
                Arg.Is<SqlParameter[]>(p =>
                    p.Length == 1 &&
                    p[0].ParameterName == "@cs_description" &&
                    (string)p[0].Value == cookingDesc
                ),
                false
            );

            _mockDataLink.Received().ExecuteNonQuery(
                Arg.Is<string>(q => q == "UpdateUserCookingSkill"),
                Arg.Is<SqlParameter[]>(p =>
                    p.Length == 2 &&
                    (int)p[0].Value == mockUserId &&
                    (int)p[1].Value == mockCookingSkillId
                )
            );
        }

        [TestMethod]
        [Obsolete]
        public void AddCookingSkill_MultipleSkills_ExecutesExpectedQueriesMultipleTimes()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string cookingDesc1 = "Grilling";
            string cookingDesc2 = "Baking";

            int mockUserId = 5;
            int mockCookingSkillId1 = 10;
            int mockCookingSkillId2 = 11;

            // Set up mock responses for both cooking skills
            _mockDataLink.ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetUserByName")),
                Arg.Is<SqlParameter[]>(p => p[0].ParameterName == "@u_name" && (string)p[0].Value == "Doe John"),
                Arg.Any<bool>()
            ).Returns(mockUserId);

            _mockDataLink.ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetCookingSkillByDescription")),
                Arg.Is<SqlParameter[]>(p => p[0].ParameterName == "@cs_description" && (string)p[0].Value == cookingDesc1),
                Arg.Any<bool>()
            ).Returns(mockCookingSkillId1);

            _mockDataLink.ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetCookingSkillByDescription")),
                Arg.Is<SqlParameter[]>(p => p[0].ParameterName == "@cs_description" && (string)p[0].Value == cookingDesc2),
                Arg.Any<bool>()
            ).Returns(mockCookingSkillId2);

            _service.AddCookingSkill(firstName, lastName, cookingDesc1);
            _service.AddCookingSkill(firstName, lastName, cookingDesc2);

            // Assert the queries were executed twice with different cooking descriptions
            _mockDataLink.Received().ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetCookingSkillByDescription")),
                Arg.Is<SqlParameter[]>(p => p[0].ParameterName == "@cs_description" && (string)p[0].Value == cookingDesc1),
                false
            );
            _mockDataLink.Received().ExecuteScalar<int>(
                Arg.Is<string>(q => q.Contains("GetCookingSkillByDescription")),
                Arg.Is<SqlParameter[]>(p => p[0].ParameterName == "@cs_description" && (string)p[0].Value == cookingDesc2),
                false
            );
        }

    }
}
