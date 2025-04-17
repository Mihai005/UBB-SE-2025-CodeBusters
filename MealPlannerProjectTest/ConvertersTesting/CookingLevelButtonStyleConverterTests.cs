using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.UI.Xaml;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using MealPlannerProject.Converters;
using Microsoft.UI.Xaml.Data;
using System.Collections.Generic;

namespace MealPlannerProjectTest.ConvertersTesting
{
    [TestClass]
    public class CookingLevelButtonStyleConverterTests
    {
        private CookingLevelButtonStyleConverter? _converter;

        [TestInitialize]
        public void Setup()
        {
            var testStyles = new Dictionary<string, object>
        {
            { "BeginnerButtonStyle", new object() },
            { "CookSometimesButtonStyle", new object() },
            { "LoveCookingButtonStyle", new object() },
            { "QuickMealsButtonStyle", new object() },
            { "MealPrepButtonStyle", new object() }
        };

            _converter = new CookingLevelButtonStyleConverter(testStyles);
        }

        [TestMethod]
        public void Convert_InvalidLevel_ReturnsNull()
        {
            var result = _converter!.Convert("Unknown", typeof(Style), null!, null!);

            Assert.IsNull(result);
        }
    }
}