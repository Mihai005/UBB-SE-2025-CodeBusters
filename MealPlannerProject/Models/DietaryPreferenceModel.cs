namespace MealPlannerProject.Models
{
    public class DietaryPreferenceModel
    {
        private string dietaryPreferences;
        private string allergies;

        public DietaryPreferenceModel(string dietaryPreferences, string allergies)
        {
            this.dietaryPreferences = dietaryPreferences;
            this.allergies = allergies;
        }
    }

    public enum DietaryPreferenceType
    {
        NotSelected = -1,
    }
}
