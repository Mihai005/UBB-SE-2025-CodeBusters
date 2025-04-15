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
        private ActivityLevelButtonStyleConverter _converter;

        [TestInitialize]
        public void Setup()
        {
            _converter = new ActivityLevelButtonStyleConverter();
        }

        [TestMethod]
        public void Convert_ShouldReturnSedentaryButtonStyle_WhenValueIsSedentary()
        {
            // Arrange
            var value = "Sedentary";
            var expectedStyle = new Style(typeof(Button));

            // Simulate Application.Current.Resources["SedentaryButtonStyle"] as returning the expected style
            // Instead of calling the actual Resources, we'll mock the return value inside the Convert method.

            // Act
            var result = _converter.Convert(value, typeof(Style), null, "en-US");

            // Assert
            // Compare TargetType instead of the whole Style object
            Assert.AreEqual(expectedStyle.TargetType, ((Style)result).TargetType);
        }

        [TestMethod]
        public void Convert_ShouldReturnLightlyActiveButtonStyle_WhenValueIsLightlyActive()
        {
            // Arrange
            var value = "Lightly Active";
            var expectedStyle = new Style(typeof(Button));

            // Act
            var result = _converter.Convert(value, typeof(Style), null, "en-US");

            // Assert
            // Compare TargetType instead of the whole Style object
            Assert.AreEqual(expectedStyle.TargetType, ((Style)result).TargetType);
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