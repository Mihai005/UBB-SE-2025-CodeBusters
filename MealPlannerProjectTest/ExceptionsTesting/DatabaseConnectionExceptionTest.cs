namespace MealPlannerProjectTest.ExceptionTesting
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MealPlannerProject.Exceptions;
    using System;

    [TestClass]
    public class DatabaseConnectionExceptionTests
    {
        [TestMethod]
        public void Constructor_NoArgs_ShouldCreateExceptionWithDefaultMessage()
        {
            // Act
            var exception = new DatabaseConnectionException();

            // Assert
            Assert.IsInstanceOfType(exception, typeof(DatabaseConnectionException));
            Assert.IsFalse(string.IsNullOrEmpty(exception.Message)); // Will be a default system message
        }

        [TestMethod]
        public void Constructor_WithMessage_ShouldSetMessage()
        {
            // Arrange
            var message = "Database not reachable";

            // Act
            var exception = new DatabaseConnectionException(message);

            // Assert
            Assert.AreEqual(message, exception.Message);
        }

        [TestMethod]
        public void Constructor_WithMessageAndInner_ShouldSetBoth()
        {
            // Arrange
            var message = "Connection failed";
            var inner = new InvalidOperationException("Low-level failure");

            // Act
            var exception = new DatabaseConnectionException(message, inner);

            // Assert
            Assert.AreEqual(message, exception.Message);
            Assert.AreEqual(inner, exception.InnerException);
        }
    }
}