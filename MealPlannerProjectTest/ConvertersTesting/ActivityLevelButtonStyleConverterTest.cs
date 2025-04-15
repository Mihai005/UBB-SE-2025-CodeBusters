using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Converters;
using Microsoft.UI.Xaml;
using System;
using Microsoft.UI.Xaml.Controls;

namespace MealPlannerProjectTest
{
    [TestClass]
    public class ActivityLevelButtonStyleConverterTests
    {
        private ActivityLevelButtonStyleConverter? _converter;

        [TestInitialize]
        public void Setup()
        {
            _converter = new ActivityLevelButtonStyleConverter();
        }

        [TestMethod]
        public void Convert_ShouldReturnNull_WhenValueIsUnknown()
        {
            // Arrange
            var value = "Unknown Activity Level";

            // Act
            var result = _converter.Convert(value, typeof(Style), null, "en-US");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBack_ShouldThrowNotImplementedException()
        {
            // Act
            _converter.ConvertBack(null, typeof(Style), null, "en-US");
        }
    }
}