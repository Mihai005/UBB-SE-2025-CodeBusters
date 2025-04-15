using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Converters;
using System;

namespace MealPlannerProjectTest.ConvertersTesting
{
    [TestClass]
    public class GoalOpacityConverterTests
    {
        private GoalOpacityConverter _converter;

        [TestInitialize]
        public void Setup()
        {
            _converter = new GoalOpacityConverter();
        }

        [TestMethod]
        public void Convert_ShouldReturn_0_5_WhenValueEqualsParameter()
        {
            // Arrange
            object value = "Goal";
            object parameter = "Goal";

            // Act
            var result = _converter.Convert(value, typeof(double), parameter, "en-US");

            // Assert
            Assert.AreEqual(0.5, result);
        }

        [TestMethod]
        public void Convert_ShouldReturn_1_0_WhenValueDoesNotEqualParameter()
        {
            // Arrange
            object value = "Goal";
            object parameter = "NotGoal";

            // Act
            var result = _converter.Convert(value, typeof(double), parameter, "en-US");

            // Assert
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBack_ShouldThrowNotImplementedException()
        {
            // Act
            _converter.ConvertBack(0.5, typeof(double), null, "en-US");
        }
    }
}
