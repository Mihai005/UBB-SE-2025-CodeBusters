namespace MealPlannerProject.Services
{
    using System;
    using MealPlannerProject.Interfaces.Repositories;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.Repositories;

    public class DietaryPreferencesService : IDietaryPreferencesService
    {
        private readonly IDietaryPreferencesRepository repository;

        public DietaryPreferencesService()
        {
            this.repository = new DietaryPreferencesRepository();
        }

        [Obsolete]
        public void AddAllergyAndDietaryPreference(string firstName, string lastName, string dietaryPreference, string allergy)
        {
            if (dietaryPreference == null && allergy == null)
            {
                throw new Exception("Please select a dietary preference and allergies!");
            }

            if (dietaryPreference == null)
            {
                throw new Exception("Please select a dietary preference!");
            }

            if (allergy == null)
            {
                throw new Exception("Please select allergies!");
            }

            this.repository.AddAllergyAndDietaryPreference(firstName, lastName, dietaryPreference, allergy);
        }
    }
}
