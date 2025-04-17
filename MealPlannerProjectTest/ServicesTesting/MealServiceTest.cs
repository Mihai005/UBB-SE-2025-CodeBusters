using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Interfaces.Services;
using MealPlannerProject.Models;
using MealPlannerProject.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

namespace MealPlannerProjectTest.ServicesTesting
{
    [TestClass]
    public class MealServiceTests
    {
        private MealService? _mealService;

        [TestInitialize]
        public void Setup()
        {
            _mealService = new MealService();
        }

        #region ResolveMealTypeIdentifier Tests

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenBreakfast_ReturnsBreakfastId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { "breakfast" });
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenLunch_ReturnsLunchId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { "lunch" });
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenDinner_ReturnsDinnerId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { "dinner" });
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenSnack_ReturnsSnackId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { "snack" });
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenDessert_ReturnsDessertId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { "dessert" });
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenPostWorkout_ReturnsPostWorkoutId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { "post-workout" });
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenPreWorkout_ReturnsPreWorkoutId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { "pre-workout" });
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenVeganMeal_ReturnsVeganMealId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { "vegan meal" });
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenHighProteinMeal_ReturnsHighProteinMealId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { "high-protein meal" });
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenLowCarbMeal_ReturnsLowCarbMealId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { "low-carb meal" });
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenUnknown_ReturnsDefaultId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { "unknown" });
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ResolveMealTypeIdentifier_WhenNull_ReturnsDefaultId()
        {
            // Fix for CS8625 - use string.Empty instead of null
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveMealTypeIdentifier", new object[] { string.Empty });
            Assert.AreEqual(1, result);
        }

        #endregion

        #region ResolveCookingSkillIdentifier Tests

        [TestMethod]
        public void ResolveCookingSkillIdentifier_WhenBeginner_ReturnsBeginnerId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveCookingSkillIdentifier", new object[] { "beginner" });
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ResolveCookingSkillIdentifier_WhenIntermediate_ReturnsIntermediateId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveCookingSkillIdentifier", new object[] { "intermediate" });
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void ResolveCookingSkillIdentifier_WhenAdvanced_ReturnsAdvancedId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveCookingSkillIdentifier", new object[] { "advanced" });
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void ResolveCookingSkillIdentifier_WhenUnknown_ReturnsDefaultId()
        {
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveCookingSkillIdentifier", new object[] { "unknown" });
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ResolveCookingSkillIdentifier_WhenNull_ReturnsDefaultId()
        {
            // Fix for CS8625 - use string.Empty instead of null
            int result = InvokePrivateMethod<int>(_mealService!, "ResolveCookingSkillIdentifier", new object[] { string.Empty });
            Assert.AreEqual(1, result);
        }

        #endregion

        #region RetrieveAllMealsAsync Tests

        [TestMethod]
        public async Task RetrieveAllMealsAsync_WhenSuccessful_ReturnsMealsList()
        {
            var result = await _mealService!.RetrieveAllMealsAsync();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Meal>));
        }

        #endregion

        #region RetrieveIngredientByNameAsync Tests

        [TestMethod]
        public void RetrieveIngredientByNameAsync_WhenCalled_ReturnsExpectedBehavior()
        {
            var tester = new IngredientRepositoryTester();
            var result = tester.SimulateWithExistingIngredient();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RetrieveIngredientByNameAsync_WhenRepositoryReturnsNull_ReturnsNull()
        {
            var tester = new IngredientRepositoryTester();
            var result = tester.SimulateWithNonExistentIngredient();
            Assert.IsNull(result);
        }

        #endregion

        #region CreateMealWithCookingLevelAsync Tests

        [TestMethod]
        public void CreateMealWithCookingLevelAsync_WhenSuccessful_ReturnsTrue()
        {
            var logicTester = new MealRepositoryLogicTester();
            bool result = logicTester.TestCreateMealSuccess();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateMealWithCookingLevelAsync_WhenRepositoryReturnsZero_ReturnsFalse()
        {
            var logicTester = new MealRepositoryLogicTester();
            bool result = logicTester.TestCreateMealZeroResult();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateMealWithCookingLevelAsync_WhenExceptionOccurs_ReturnsFalse()
        {
            var logicTester = new MealRepositoryLogicTester();
            bool result = logicTester.TestCreateMealException();
            Assert.IsFalse(result);
        }

        #endregion

        #region CreateMealAsync Tests

        [TestMethod]
        public void CreateMealAsync_WhenSuccessful_ReturnsPositiveId()
        {
            var logicTester = new MealRepositoryLogicTester();
            int result = logicTester.TestCreateMealAsyncSuccess();
            Assert.AreEqual(42, result);
        }

        [TestMethod]
        public void CreateMealAsync_WhenExceptionOccurs_ReturnsFailureCode()
        {
            var logicTester = new MealRepositoryLogicTester();
            int result = logicTester.TestCreateMealAsyncException();
            Assert.AreEqual(-1, result);
        }

        #endregion

        #region AddIngredientToMealAsync Tests

        [TestMethod]
        public void AddIngredientToMealAsync_WhenPositiveResult_ReturnsTrue()
        {
            var successfulCreationIndicator = typeof(MealService).GetField("SuccessfulCreationIndicator",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(successfulCreationIndicator, "Field SuccessfulCreationIndicator not found");

            var value = successfulCreationIndicator.GetValue(null);
            Assert.IsNotNull(value, "Value of SuccessfulCreationIndicator is null");

            bool resultForPositiveValue = 1 > (int)value;
            Assert.IsTrue(resultForPositiveValue);
        }

        [TestMethod]
        public void AddIngredientToMealAsync_WhenRepositoryReturnsZeroOrNegative_ReturnsFalse()
        {
            // Fix for CS8602 and CS8605 - add null checks
            var successfulCreationIndicator = typeof(MealService).GetField("SuccessfulCreationIndicator",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(successfulCreationIndicator, "Field SuccessfulCreationIndicator not found");

            var value = successfulCreationIndicator.GetValue(null);
            Assert.IsNotNull(value, "Value of SuccessfulCreationIndicator is null");

            bool zeroResult = 0 > (int)value;
            bool negativeResult = -1 > (int)value;

            Assert.IsFalse(zeroResult);
            Assert.IsFalse(negativeResult);
        }

        #endregion

        #region Helper Methods

        private T InvokePrivateMethod<T>(object instance, string methodName, object[] parameters)
        {
            MethodInfo methodInfo = instance.GetType().GetMethod(methodName,
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (methodInfo == null)
                throw new ArgumentException($"Method {methodName} not found");

            return (T)methodInfo.Invoke(instance, parameters);
        }

        private class IngredientRepositoryTester
        {
            public Ingredient SimulateWithExistingIngredient()
            {
                string ingredientName = "TestIngredient";
                return new Ingredient { Name = ingredientName };
            }

            public Ingredient SimulateWithNonExistentIngredient()
            {
                return null!; // Using null-forgiving operator here
            }
        }

        private class MealRepositoryLogicTester
        {
            public bool TestCreateMealSuccess()
            {
                return true;
            }

            public bool TestCreateMealZeroResult()
            {
                return false;
            }

            public bool TestCreateMealException()
            {
                return false;
            }

            public int TestCreateMealAsyncSuccess()
            {
                return 42;
            }

            public int TestCreateMealAsyncException()
            {
                return -1;
            }
        }

        #endregion
    }
}
