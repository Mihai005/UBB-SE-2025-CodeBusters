using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Services;
using MealPlannerProject.Queries;
using MealPlannerProject.Interfaces;
using System.Data;
using System.Data.SqlClient;
using Moq;
using System.Reflection;

namespace MealPlannerProjectTest.Services
{
    [TestClass]
    public class UserPageServiceTests
    {
        private UserPageService? _userPageService;
        private Mock<IDataLink>? _dataLinkMock;

        [TestInitialize]
        public void Setup()
        {
            _dataLinkMock = new Mock<IDataLink>();
            _userPageService = new UserPageService(_dataLinkMock.Object);
        }

        #region UserHasAnAccount Tests

        [TestMethod]
        public void UserHasAnAccount_WhenUserExists_ReturnsUserId()
        {
            // Arrange
            _dataLinkMock!.Setup(dl => dl.ExecuteScalar<int?>("SELECT dbo.GetUserByName(@u_name)", 
                It.IsAny<SqlParameter[]>(), false))
                .Returns(5);

            // Act
            int result = _userPageService!.UserHasAnAccount("John");

            // Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void UserHasAnAccount_WhenUserDoesNotExist_ReturnsNegativeOne()
        {
            // Arrange
            _dataLinkMock!.Setup(dl => dl.ExecuteScalar<int?>("SELECT dbo.GetUserByName(@u_name)",
                It.IsAny<SqlParameter[]>(), false))
                .Returns((int?)null);

            // Act
            int result = _userPageService!.UserHasAnAccount("NonExistent");

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void UserHasAnAccount_WhenUserIdIsZero_ReturnsNegativeOne()
        {
            // Arrange
            _dataLinkMock!.Setup(dl => dl.ExecuteScalar<int?>("SELECT dbo.GetUserByName(@u_name)",
                It.IsAny<SqlParameter[]>(), false))
                .Returns(0);

            // Act
            int result = _userPageService!.UserHasAnAccount("ZeroUser");

            // Assert
            Assert.AreEqual(-1, result);
        }

        #endregion

        #region InsertNewUser Tests

        [TestMethod]
        public void InsertNewUser_WhenCalled_ReturnsUserIdFromOutputParameter()
        {
            // Arrange
            int expectedId = 10;
            _dataLinkMock!.Setup(dl => dl.ExecuteNonQuery("InsertNewUser", It.IsAny<SqlParameter[]>()))
                .Callback<string, SqlParameter[]>((_, parameters) =>
                {
                    // Simulate stored procedure setting the output parameter
                    parameters[1].Value = expectedId;
                });

            // Act
            int result = _userPageService!.InsertNewUser("NewUser");

            // Assert
            Assert.AreEqual(expectedId, result);
        }

        #endregion
    }
}
