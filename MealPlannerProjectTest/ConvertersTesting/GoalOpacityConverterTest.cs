using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Converters;
using System;

namespace MealPlannerProjectTest.Converters
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
        public void Convert_MatchingValues_ReturnsHalfOpacity()
        {
            // Arrange
            var value = "Lose Weight";
            var parameter = "Lose Weight";

            // Act
            var result = _converter.Convert(value, null, parameter, null);

            // Assert
            Assert.AreEqual(0.5, result);
        }

        [TestMethod]
        public void Convert_NonMatchingValues_ReturnsFullOpacity()
        {
            // Arrange
            var value = "Maintain Weight";
            var parameter = "Lose Weight";

            // Act
            var result = _converter.Convert(value, null, parameter, null);

            // Assert
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void Convert_NullValue_ReturnsFullOpacity()
        {
            // Arrange
            object value = null;
            var parameter = "Lose Weight";

            // Act
            var result = _converter.Convert(value, null, parameter, null);

            // Assert
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void Convert_NullParameter_ReturnsFullOpacity()
        {
            // Arrange
            var value = "Lose Weight";
            object parameter = null;

            // Act
            var result = _converter.Convert(value, null, parameter, null);

            // Assert
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBack_ThrowsNotImplementedException()
        {
            // Act
            _converter.ConvertBack(1.0, null, null, null);
        }
    }
}
