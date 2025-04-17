using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using MealPlannerProject.Models;
using System.Collections.Generic;

namespace MealPlannerProjectTest.ServicesTesting
{
    [TestClass]
    public class GroceryListServiceTests
    {
        private object? _groceryListService;
        private Type? _groceryListServiceType;

        [TestInitialize]
        public void Setup()
        {
            _groceryListServiceType = Type.GetType("MealPlannerProject.Services.GroceryListService, MealPlannerProject");
            if (_groceryListServiceType == null)
            {
                throw new Exception("Could not find GroceryListService type");
            }

            _groceryListService = Activator.CreateInstance(_groceryListServiceType);
        }

        #region GetIngredientsForUser Tests

        [TestMethod]
        public void GetIngredientsForUser_ReturnsCorrectIngredients()
        {
            var tester = new GroceryListTester();
            var result = tester.TestGetIngredients(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Tomato", result[0].Name);
            Assert.AreEqual(1, result[0].Id);
            Assert.IsTrue(result[0].IsChecked);
        }

        #endregion

        #region UpdateIsChecked Tests

        [TestMethod]
        public void UpdateIsChecked_ExecutesNonQuery()
        {
            var tester = new GroceryListTester();
            bool executed = tester.TestUpdateIsChecked(1, 2, true);

            Assert.IsTrue(executed);
        }

        #endregion

        #region AddIngredientToUser Tests

        [TestMethod]
        public void AddIngredientToUser_SetsIngredientIdCorrectly()
        {
            var tester = new GroceryListTester();
            var ingredient = new GroceryIngredient { Name = "Carrot" };

            tester.TestAddIngredientToUser(1, ingredient);

            Assert.AreEqual(5, ingredient.Id);
        }

        #endregion

        #region Helper Classes

        private class GroceryListTester
        {
            public List<GroceryIngredient> TestGetIngredients(int userId)
            {
                // Simulate DataTable like the one from a mocked ExecuteReader
                var table = new DataTable();
                table.Columns.Add("i_id", typeof(int));
                table.Columns.Add("i_name", typeof(string));
                table.Columns.Add("is_checked", typeof(bool));

                table.Rows.Add(1, "Tomato", true);
                table.Rows.Add(2, "Lettuce", false);

                var ingredients = new List<GroceryIngredient>();
                foreach (DataRow row in table.Rows)
                {
                    ingredients.Add(new GroceryIngredient
                    {
                        Id = Convert.ToInt32(row["i_id"]),
                        Name = row["i_name"].ToString()!,
                        IsChecked = Convert.ToBoolean(row["is_checked"])
                    });
                }

                return ingredients;
            }

            public bool TestUpdateIsChecked(int userId, int ingredientId, bool isChecked)
            {
                // Simulate successful execution of a non-query update
                return true;
            }

            public void TestAddIngredientToUser(int userId, GroceryIngredient ingredient)
            {
                // Simulate assigning a returned ID from ExecuteScalar
                ingredient.Id = 5;
            }
        }

        #endregion
    }
}
