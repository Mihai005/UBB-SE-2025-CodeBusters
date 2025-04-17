using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealPlannerProject.Interfaces.Services;
using MealPlannerProject.Services;
using System;

namespace MealPlannerProjectTest.ServicesTesting
{

    [TestClass]
    public class DietaryPreferencesServiceTest
    {
        private IDietaryPreferencesService? service;
        private string? mockFirstName;
        private string? mockLastName;
        [TestInitialize]
        public void Setup()
        {
            service = new DietaryPreferencesService();
            mockFirstName = "mockFN";
            mockLastName = "mockLN";
        }
        [TestMethod]
        public void AddAllergyAndDietaryPreferenceTest()
        {
            try
            {
                service.AddAllergyAndDietaryPreference(mockFirstName, mockLastName, "", "");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals("Please select a dietary preference and allergies!"));
            }

            try
            {
                service.AddAllergyAndDietaryPreference(mockFirstName, mockLastName, "notEmpty", "");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals("Please select allergies!"));
            }

            try
            {
                service.AddAllergyAndDietaryPreference(mockFirstName, mockLastName, "", "notEmpty");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals("Please select a dietary preference!"));
            }
        }
    }
}