using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Services;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MealPlannerProjectTest.Services
{
    [TestClass]
    public class CalorieServiceTests
    {
        private CalorieService? _calorieService;
        private DataLinkMock? _dataLinkMock;

        [TestInitialize]
        public void Setup()
        {
            _dataLinkMock = new DataLinkMock();
            _calorieService = new CalorieService(_dataLinkMock);
        }

        #region GetGoal Tests

        [TestMethod]
        public void GetGoal_ValidUserId_ReturnsCalorieGoal()
        {
            // Arrange
            const int userId = 1;
            const float expectedGoal = 2000f;
            _dataLinkMock!.SetupExecuteScalarResult(expectedGoal);

            // Act
            float result = _calorieService!.GetGoal(userId);

            // Assert
            Assert.AreEqual(expectedGoal, result);
            Assert.IsTrue(_dataLinkMock.VerifyExecuteScalarCalled());
        }

        [TestMethod]
        public void GetGoal_NullFromDatabase_ReturnsZero()
        {
            // Arrange
            _dataLinkMock!.SetupExecuteScalarResult(null);

            // Act
            float result = _calorieService!.GetGoal(1);

            // Assert
            Assert.AreEqual(0f, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetGoal_DatabaseError_ThrowsException()
        {
            // Arrange
            _dataLinkMock!.SetupExecuteScalarException(new SqlException());

            // Act
            _calorieService!.GetGoal(1);
        }

        #endregion

        #region GetFood Tests

        [TestMethod]
        public void GetFood_ValidUserId_ReturnsCaloriesFromFood()
        {
            // Arrange
            const float expectedCalories = 1500f;
            _dataLinkMock!.SetupExecuteScalarResult(expectedCalories);

            // Act
            float result = _calorieService!.GetFood(1);

            // Assert
            Assert.AreEqual(expectedCalories, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetFood_DatabaseError_ThrowsException()
        {
            // Arrange
            _dataLinkMock!.SetupExecuteScalarException(new SqlException());

            // Act
            _calorieService!.GetFood(1);
        }

        #endregion

        #region GetExercise Tests

        [TestMethod]
        public void GetExercise_ValidUserId_ReturnsCaloriesBurned()
        {
            // Arrange
            const float expectedBurn = 500f;
            _dataLinkMock!.SetupExecuteScalarResult(expectedBurn);

            // Act
            float result = _calorieService!.GetExercise(1);

            // Assert
            Assert.AreEqual(expectedBurn, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetExercise_DatabaseError_ThrowsException()
        {
            // Arrange
            _dataLinkMock!.SetupExecuteScalarException(new SqlException());

            // Act
            _calorieService!.GetExercise(1);
        }

        #endregion

        #region Mock Class

        private class DataLinkMock : IDataLink
        {
            private object? _executeScalarResult;
            private Exception? _executeScalarException;
            private bool _executeScalarCalled;

            public void SetupExecuteScalarResult(object? result)
            {
                _executeScalarResult = result;
                _executeScalarException = null;
                _executeScalarCalled = false;
            }

            public void SetupExecuteScalarException(Exception exception)
            {
                _executeScalarException = exception;
                _executeScalarResult = null;
            }

            public bool VerifyExecuteScalarCalled() => _executeScalarCalled;

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
                throw new NotImplementedException();
            }

            public int ExecuteQuery(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure)
            {
                throw new NotImplementedException();
            }

            public DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
