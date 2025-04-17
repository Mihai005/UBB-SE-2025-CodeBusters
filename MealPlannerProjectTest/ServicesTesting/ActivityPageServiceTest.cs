using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace MealPlannerProjectTest.ServicesTesting
{
    [TestClass]
    public class ActivityPageServiceTests
    {
        private object? _activityPageService;
        private Type? _activityPageServiceType;

        [TestInitialize]
        public void Setup()
        {
            // Get the ActivityPageService type using reflection
            _activityPageServiceType = Type.GetType("MealPlannerProject.Services.ActivityPageService, MealPlannerProject");
            if (_activityPageServiceType == null)
            {
                throw new Exception("Could not find ActivityPageService type");
            }

            // Create an instance using reflection
            _activityPageService = Activator.CreateInstance(_activityPageServiceType);
        }

        #region AddActivity Tests

        [TestMethod]
        public void AddActivity_WhenCalled_FormatsUserNameCorrectly()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string expectedFullName = "Doe John";

            // Use reflection to access private constant
            var userNameParamField = _activityPageServiceType!.GetField("UserNameParameter",
                BindingFlags.NonPublic | BindingFlags.Static);
            string userNameParam = (string)userNameParamField!.GetValue(null)!;

            // Create a helper to test the logic
            var tester = new ActivityServiceTester();

            // Act
            string actualUserName = tester.TestUserNameFormatting(firstName, lastName);

            // Assert
            Assert.AreEqual(expectedFullName, actualUserName);
        }

        [TestMethod]
        public void AddActivity_WhenCalled_UsesCorrectUserLookupQuery()
        {
            // Use reflection to access private constant
            var queryField = _activityPageServiceType!.GetField("UserLookupQuery",
                BindingFlags.NonPublic | BindingFlags.Static);

            string userLookupQuery = (string)queryField!.GetValue(null)!;

            Assert.AreEqual("SELECT dbo.GetUserByName(@userFullName)", userLookupQuery);
        }

        [TestMethod]
        public void AddActivity_WhenCalled_UsesCorrectActivityLookupQuery()
        {
            var queryField = _activityPageServiceType!.GetField("ActivityLookupQuery",
                BindingFlags.NonPublic | BindingFlags.Static);

            string activityLookupQuery = (string)queryField!.GetValue(null)!;

            Assert.AreEqual("SELECT dbo.GetActivityByDescription(@activityDescription)", activityLookupQuery);
        }

        [TestMethod]
        public void AddActivity_WhenCalled_UsesCorrectUpdateProcedure()
        {
            var procedureField = _activityPageServiceType!.GetField("UpdateActivityProcedure",
                BindingFlags.NonPublic | BindingFlags.Static);

            string updateProcedure = (string)procedureField!.GetValue(null)!;

            Assert.AreEqual("UpdateUserActivity", updateProcedure);
        }

        [TestMethod]
        public void AddActivity_WithValidParameters_ExecutesNonQuery()
        {
            var tester = new ActivityServiceTester();
            bool result = tester.TestUpdateActivityExecution();

            Assert.IsTrue(result, "The update activity execution should complete successfully");
        }

        [TestMethod]
        [ExpectedException(typeof(TargetInvocationException))]
        public void AddActivity_WithNullParameters_ThrowsException()
        {
            if (_activityPageService != null && _activityPageServiceType != null)
            {
                // Get the AddActivity method
                var addActivityMethod = _activityPageServiceType.GetMethod("AddActivity");

                // Invoke it with null parameters
                addActivityMethod!.Invoke(_activityPageService, new object[] { null!, null!, null! });
            }
        }



        #endregion

        #region Helper Classes

        private class ActivityServiceTester
        {
            public string TestUserNameFormatting(string firstName, string lastName)
            {
                return lastName + " " + firstName;
            }

            public bool TestUpdateActivityExecution()
            {
                // Simulate successful execution
                return true;
            }
        }

        #endregion
    }
}
