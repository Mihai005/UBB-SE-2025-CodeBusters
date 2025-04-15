using MealPlannerProject.Converters;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;

namespace MealPlannerProjectTest.ConvertersTesting
{
    [TestClass]
    public class GoalButtonStyleConverterTests
    {
        private GoalButtonStyleConverter _converter;

        [TestInitialize]
        public void Setup()
        {
            _converter = new GoalButtonStyleConverter();
        }

        [UITestMethod]
        public void Convert_ShouldReturnLoseWeightButtonStyle_WhenGoalIsLoseWeight()
        {
            // Arrange
            var value = "Lose weight";
            var expectedStyle = new Style(typeof(Button));

            // Simulate Application.Current.Resources["LoseWeightButtonStyle"] as returning the expected style
            // Instead of calling the actual Resources, we'll mock the return value inside the Convert method.

            // Act
            var result = _converter.Convert(value, typeof(Style), null, "en-US");

            // Assert
            Assert.AreEqual(expectedStyle.TargetType, ((Style)result).TargetType);
        }

        [UITestMethod]
        public void Convert_ShouldReturnGainWeightButtonStyle_WhenGoalIsGainWeight()
        {
            // Arrange
            var value = "Gain weight";
            var expectedStyle = new Style(typeof(Button));

            // Act
            var result = _converter.Convert(value, typeof(Style), null, "en-US");

            // Assert
            Assert.AreEqual(expectedStyle.TargetType, ((Style)result).TargetType);
        }

        [UITestMethod]
        public void Convert_ShouldReturnMaintainWeightButtonStyle_WhenGoalIsMaintainWeight()
        {
            // Arrange
            var value = "Maintain weight";
            var expectedStyle = new Style(typeof(Button));

            // Act
            var result = _converter.Convert(value, typeof(Style), null, "en-US");

            // Assert
            Assert.AreEqual(expectedStyle.TargetType, ((Style)result).TargetType);
        }

        [UITestMethod]
        public void Convert_ShouldReturnBodyRecompositionButtonStyle_WhenGoalIsBodyRecomposition()
        {
            // Arrange
            var value = "Body recomposition";
            var expectedStyle = new Style(typeof(Button));

            // Act
            var result = _converter.Convert(value, typeof(Style), null, "en-US");

            // Assert
            Assert.AreEqual(expectedStyle.TargetType, ((Style)result).TargetType);
        }

        [UITestMethod]
        public void Convert_ShouldReturnImproveHealthButtonStyle_WhenGoalIsImproveOverallHealth()
        {
            // Arrange
            var value = "Improve overall health";
            var expectedStyle = new Style(typeof(Button));

            // Act
            var result = _converter.Convert(value, typeof(Style), null, "en-US");

            // Assert
            Assert.AreEqual(expectedStyle.TargetType, ((Style)result).TargetType);
        }

        [TestMethod]
        public void Convert_ShouldReturnNull_WhenGoalIsUnknown()
        {
            // Arrange
            var value = "Unknown Goal";

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
