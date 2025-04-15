using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Services;
using MealPlannerProject.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MealPlannerProjectTest.Services
{
    [TestClass]
    public class CalorieServiceTests
    {
        public CalorieService? _calorieService;
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
            const int userId = 1;
            const float expectedGoal = 2000f;
            _dataLinkMock!.SetupExecuteScalarResult(expectedGoal);

            float result = _calorieService!.GetGoal(userId);

            Assert.AreEqual(expectedGoal, result);
            Assert.IsTrue(_dataLinkMock.VerifyExecuteScalarCalled());
        }

        [TestMethod]
        public void GetGoal_NullFromDatabase_ReturnsZero()
        {
            _dataLinkMock!.SetupExecuteScalarResult(null);

            float result = _calorieService!.GetGoal(1);

            Assert.AreEqual(0f, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetGoal_DatabaseError_ThrowsException()
        {
            _dataLinkMock!.SetupExecuteScalarException(new InvalidOperationException("Simulated database error"));

            _calorieService!.GetGoal(1);
        }

        #endregion

        #region GetFood Tests

        [TestMethod]
        public void GetFood_ValidUserId_ReturnsCaloriesFromFood()
        {
            const float expectedCalories = 1500f;
            _dataLinkMock!.SetupExecuteScalarResult(expectedCalories);

            float result = _calorieService!.GetFood(1);

            Assert.AreEqual(expectedCalories, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetFood_DatabaseError_ThrowsException()
        {
            _dataLinkMock!.SetupExecuteScalarException(new InvalidOperationException("Simulated database error"));

            _calorieService!.GetFood(1);
        }

        #endregion

        #region GetExercise Tests

        [TestMethod]
        public void GetExercise_ValidUserId_ReturnsCaloriesBurned()
        {
            const float expectedBurn = 500f;
            _dataLinkMock!.SetupExecuteScalarResult(expectedBurn);

            float result = _calorieService!.GetExercise(1);

            Assert.AreEqual(expectedBurn, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetExercise_DatabaseError_ThrowsException()
        {
            _dataLinkMock!.SetupExecuteScalarException(new InvalidOperationException("Simulated database error"));

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

            public T? ExecuteScalar<T>(string query, System.Data.SqlClient.SqlParameter[]? sqlParameters, bool isStoredProcedure)
            {
                _executeScalarCalled = true;

                if (_executeScalarException != null)
                    throw _executeScalarException;

                if (_executeScalarResult == null)
                    return default;

                return (T)_executeScalarResult;
            }

            public int ExecuteNonQuery(string storedProcedure, System.Data.SqlClient.SqlParameter[]? sqlParameters)
            {
                throw new NotImplementedException();
            }

            public int ExecuteQuery(string query, System.Data.SqlClient.SqlParameter[]? sqlParameters, bool isStoredProcedure)
            {
                throw new NotImplementedException();
            }

            public DataTable ExecuteSqlQuery(string query, System.Data.SqlClient.SqlParameter[]? sqlParameters)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
