using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Interfaces;
using MealPlannerProject.Interfaces.Services;
using MealPlannerProject.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MealPlannerProjectTest.Services
{
    [TestClass]
    public class UserPageServiceTests
    {
        private UserPageService? _userPageService;
        private DataLinkMock? _dataLinkMock;

        [TestInitialize]
        public void Setup()
        {
            _dataLinkMock = new DataLinkMock();
            _userPageService = new UserPageService(_dataLinkMock);
        }

        #region CheckUserExists Tests

        [TestMethod]
        public void CheckUserExists_UserFound_ReturnsUserId()
        {
            // Arrange
            const string username = "testUser";
            const int expectedUserId = 42;
            _dataLinkMock!.SetupExecuteScalarResult(expectedUserId);

            // Act
            int result = _userPageService!.CheckUserExists(username);

            // Assert
            Assert.AreEqual(expectedUserId, result);
            Assert.IsTrue(_dataLinkMock.VerifyExecuteScalarCalled());
        }

        [TestMethod]
        public void CheckUserExists_UserNotFound_ReturnsNegativeOne()
        {
            // Arrange
            const string username = "nonExistentUser";
            _dataLinkMock!.SetupExecuteScalarResult(0);

            // Act
            int result = _userPageService!.CheckUserExists(username);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void CheckUserExists_NullReturnFromDatabase_ReturnsNegativeOne()
        {
            // Arrange
            const string username = "testUser";
            _dataLinkMock!.SetupExecuteScalarResult(null);

            // Act
            int result = _userPageService!.CheckUserExists(username);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckUserExists_EmptyUsername_ThrowsArgumentException()
        {
            // Act - will throw exception
            _userPageService!.CheckUserExists(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckUserExists_WhitespaceUsername_ThrowsArgumentException()
        {
            // Act - will throw exception
            _userPageService!.CheckUserExists("   ");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CheckUserExists_DatabaseError_ThrowsInvalidOperationException()
        {
            // Arrange
            const string username = "testUser";
            _dataLinkMock!.SetupExecuteScalarException(new SqlException());

            // Act - will throw exception
            _userPageService!.CheckUserExists(username);
        }

        #endregion

        #region CreateNewUser Tests

        [TestMethod]
        public void CreateNewUser_ValidName_ReturnsNewUserId()
        {
            // Arrange
            const string username = "newUser";
            const int expectedUserId = 123;
            _dataLinkMock!.SetupExecuteNonQueryOutput(expectedUserId);

            // Act
            int result = _userPageService!.CreateNewUser(username);

            // Assert
            Assert.AreEqual(expectedUserId, result);
            Assert.IsTrue(_dataLinkMock.VerifyExecuteNonQueryCalled());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateNewUser_EmptyUsername_ThrowsArgumentException()
        {
            // Act - will throw exception
            _userPageService!.CreateNewUser(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateNewUser_WhitespaceUsername_ThrowsArgumentException()
        {
            // Act - will throw exception
            _userPageService!.CreateNewUser("   ");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateNewUser_DatabaseError_ThrowsInvalidOperationException()
        {
            // Arrange
            const string username = "newUser";
            _dataLinkMock!.SetupExecuteNonQueryException(new SqlException());

            // Act - will throw exception
            _userPageService!.CreateNewUser(username);
        }

        #endregion

        #region Async Tests

        [TestMethod]
        public async Task CheckUserExistsAsync_UserFound_ReturnsUserId()
        {
            // Arrange
            const string username = "testUser";
            const int expectedUserId = 42;
            _dataLinkMock!.SetupExecuteScalarResult(expectedUserId);

            // Act
            int result = await _userPageService!.CheckUserExistsAsync(username);

            // Assert
            Assert.AreEqual(expectedUserId, result);
            Assert.IsTrue(_dataLinkMock.VerifyExecuteScalarCalled());
        }

        [TestMethod]
        public async Task CreateNewUserAsync_ValidName_ReturnsNewUserId()
        {
            // Arrange
            const string username = "newUser";
            const int expectedUserId = 123;
            _dataLinkMock!.SetupExecuteNonQueryOutput(expectedUserId);

            // Act
            int result = await _userPageService!.CreateNewUserAsync(username);

            // Assert
            Assert.AreEqual(expectedUserId, result);
            Assert.IsTrue(_dataLinkMock.VerifyExecuteNonQueryCalled());
        }

        #endregion

        #region Mock Classes

        private class DataLinkMock : IDataLink
        {
            private object? _executeScalarResult;
            private Exception? _executeScalarException;
            private bool _executeScalarCalled;
            
            private int _executeNonQueryOutput;
            private Exception? _executeNonQueryException;
            private bool _executeNonQueryCalled;

            public void SetupExecuteScalarResult(object? result)
            {
                _executeScalarResult = result;
                _executeScalarException = null;
            }

            public void SetupExecuteScalarException(Exception exception)
            {
                _executeScalarException = exception;
                _executeScalarResult = null;
            }

            public void SetupExecuteNonQueryOutput(int outputValue)
            {
                _executeNonQueryOutput = outputValue;
                _executeNonQueryException = null;
            }

            public void SetupExecuteNonQueryException(Exception exception)
            {
                _executeNonQueryException = exception;
            }

            public bool VerifyExecuteScalarCalled() => _executeScalarCalled;
            public bool VerifyExecuteNonQueryCalled() => _executeNonQueryCalled;

            public T? ExecuteScalar<T>(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure)
            {
                _executeScalarCalled = true;

                if (_executeScalarException != null)
                    throw _executeScalarException;

                if (_executeScalarResult == null)
                    return default;

                return (T)_executeScalarResult;
            }

            public int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters)
            {
                _executeNonQueryCalled = true;

                if (_executeNonQueryException != null)
                    throw _executeNonQueryException;

                // Set the output parameter value if sqlParameters is not null
                if (sqlParameters != null)
                {
                    foreach (var param in sqlParameters)
                    {
                        if (param.Direction == ParameterDirection.Output && param.ParameterName == "@id")
                        {
                            param.Value = _executeNonQueryOutput;
                            break;
                        }
                    }
                }

                return 1; // Indicating success
            }

            public int ExecuteQuery(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure)
            {
                // Not used in these tests
                return 0;
            }

            public DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters)
            {
                // Not used in these tests
                return new DataTable();
            }
        }

        #endregion
    }
}