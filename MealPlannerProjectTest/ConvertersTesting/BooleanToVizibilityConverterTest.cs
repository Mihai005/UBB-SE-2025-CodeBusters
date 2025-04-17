namespace MealPlannerProjectTest.ConvertersTesting
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.UI.Xaml;
    using MealPlannerProject.Converters;

    [TestClass]
    public class BooleanToVisibilityConverterTests
    {
        private BooleanToVisibilityConverter _converter;

        [TestInitialize]
        public void Setup()
        {
            _converter = new BooleanToVisibilityConverter();
        }

        [TestMethod]
        public void Convert_ShouldReturnVisible_WhenValueIsTrue()
        {
            // Arrange
            object value = true;

            // Act
            var result = _converter.Convert(value, typeof(Visibility), null, null);

            // Assert
            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod]
        public void Convert_ShouldReturnCollapsed_WhenValueIsFalse()
        {
            // Arrange
            object value = false;

            // Act
            var result = _converter.Convert(value, typeof(Visibility), null, null);

            // Assert
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void Convert_ShouldReturnCollapsed_WhenValueIsNotBoolean()
        {
            // Arrange
            object value = "not a bool";

            // Act
            var result = _converter.Convert(value, typeof(Visibility), null, null);

            // Assert
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void ConvertBack_ShouldReturnTrue_WhenVisibilityIsVisible()
        {
            // Arrange
            object value = Visibility.Visible;

            // Act
            var result = _converter.ConvertBack(value, typeof(bool), null, null);

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ConvertBack_ShouldReturnFalse_WhenVisibilityIsCollapsed()
        {
            // Arrange
            object value = Visibility.Collapsed;

            // Act
            var result = _converter.ConvertBack(value, typeof(bool), null, null);

            // Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ConvertBack_ShouldReturnFalse_WhenValueIsNotVisibility()
        {
            // Arrange
            object value = "not a visibility";

            // Act
            var result = _converter.ConvertBack(value, typeof(bool), null, null);

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}