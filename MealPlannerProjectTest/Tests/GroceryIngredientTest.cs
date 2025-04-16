using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Models;
using System.ComponentModel;

namespace MealPlannerProjectTest.Models
{
    [TestClass]
    public class GroceryIngredientTests
    {
        [TestMethod]
        public void SetName_PropertyUpdatesCorrectly()
        {
            // Arrange
            var ingredient = new GroceryIngredient();
            string expectedName = "Tomato";

            // Act
            ingredient.Name = expectedName;

            // Assert
            Assert.AreEqual(expectedName, ingredient.Name);
        }

        [TestMethod]
        public void SetIsChecked_PropertyUpdatesCorrectly()
        {
            // Arrange
            var ingredient = new GroceryIngredient();

            // Act
            ingredient.IsChecked = true;

            // Assert
            Assert.IsTrue(ingredient.IsChecked);
        }

        [TestMethod]
        public void PropertyChanged_EventFiresWhenNameChanges()
        {
            // Arrange
            var ingredient = new GroceryIngredient();
            bool eventFired = false;
            ingredient.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(GroceryIngredient.Name))
                {
                    eventFired = true;
                }
            };

            // Act
            ingredient.Name = "Cheese";

            // Assert
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void PropertyChanged_EventFiresWhenIsCheckedChanges()
        {
            // Arrange
            var ingredient = new GroceryIngredient();
            bool eventFired = false;
            ingredient.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(GroceryIngredient.IsChecked))
                {
                    eventFired = true;
                }
            };

            // Act
            ingredient.IsChecked = true;

            // Assert
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void DefaultIngredient_HasExpectedValues()
        {
            // Arrange
            var defaultIng = GroceryIngredient.defaultIngredient;

            // Assert
            Assert.AreEqual(-1, defaultIng.Id);
            Assert.AreEqual("", defaultIng.Name);
            Assert.IsFalse(defaultIng.IsChecked);
        }
    }
}
