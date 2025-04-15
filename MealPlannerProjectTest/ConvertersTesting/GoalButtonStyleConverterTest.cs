namespace MealPlannerProjectTest.ConvertersTesting
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MealPlannerProject.Converters;
    using System.Collections.Generic;
    using System;

    public class MockResourceProvider : IResourceProvider
    {
        private readonly Dictionary<string, object> _resources;

        public MockResourceProvider()
        {
            _resources = new Dictionary<string, object>
            {
                { "LoseWeightButtonStyle", new object() },
                { "GainWeightButtonStyle", new object() },
                { "MaintainWeightButtonStyle", new object() },
                { "BodyRecompositionButtonStyle", new object() },
                { "ImproveHealthButtonStyle", new object() }
            };
        }

        public object? Get(string key) => _resources.TryGetValue(key, out var value) ? value : null;

        public object GetByKey(string key) => _resources[key];
    }

    [TestClass]
    public class GoalButtonStyleConverterTests
    {
        private GoalButtonStyleConverter _converter;
        private MockResourceProvider _mockResources;

        [TestInitialize]
        public void Setup()
        {
            _mockResources = new MockResourceProvider();
            _converter = new GoalButtonStyleConverter
            {
                ResourceProvider = _mockResources
            };
        }

        [TestMethod]
        public void Convert_ShouldReturnLoseWeightStyle_WhenGoalIsLoseWeight()
        {
            var result = _converter.Convert("Lose weight", typeof(object), null, null);
            Assert.AreEqual(_mockResources.GetByKey("LoseWeightButtonStyle"), result);
        }

        [TestMethod]
        public void Convert_ShouldReturnGainWeightStyle_WhenGoalIsGainWeight()
        {
            var result = _converter.Convert("Gain weight", typeof(object), null, null);
            Assert.AreEqual(_mockResources.GetByKey("GainWeightButtonStyle"), result);
        }

        [TestMethod]
        public void Convert_ShouldReturnMaintainWeightStyle_WhenGoalIsMaintainWeight()
        {
            var result = _converter.Convert("Maintain weight", typeof(object), null, null);
            Assert.AreEqual(_mockResources.GetByKey("MaintainWeightButtonStyle"), result);
        }

        [TestMethod]
        public void Convert_ShouldReturnBodyRecompositionStyle_WhenGoalIsBodyRecomposition()
        {
            var result = _converter.Convert("Body recomposition", typeof(object), null, null);
            Assert.AreEqual(_mockResources.GetByKey("BodyRecompositionButtonStyle"), result);
        }

        [TestMethod]
        public void Convert_ShouldReturnImproveHealthStyle_WhenGoalIsImproveOverallHealth()
        {
            var result = _converter.Convert("Improve overall health", typeof(object), null, null);
            Assert.AreEqual(_mockResources.GetByKey("ImproveHealthButtonStyle"), result);
        }

        [TestMethod]
        public void Convert_ShouldReturnNull_WhenGoalIsUnknown()
        {
            var result = _converter.Convert("Some unknown goal", typeof(object), null, null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Convert_ShouldReturnNull_WhenGoalIsNull()
        {
            var result = _converter.Convert(null, typeof(object), null, null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Convert_ShouldReturnNull_WhenGoalIsNotAString()
        {
            var result = _converter.Convert(123, typeof(object), null, null);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBack_ShouldThrowNotImplementedException()
        {
            _converter.ConvertBack(null, typeof(object), null, null);
        }
    }
}
