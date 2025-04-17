namespace MealPlannerProject.Models
{
    using System.Collections.ObjectModel;

    public class MealCreate
    {
        public string MealName { get; set; }

        public string CookingTime { get; set; }

        public ObservableCollection<string> CookingDirections { get; set; }

        public ObservableCollection<string> MealIngredients { get; set; }

        public int CalorieCount { get; set; }

        public int ProteinGrams { get; set; }

        public int CarbohydrateGrams { get; set; }

        public int FatGrams { get; set; }

        public int FiberGrams { get; set; }

        public int SugarGrams { get; set; }

        public MealCreate()
        {
            this.MealName = string.Empty;
            this.CookingTime = string.Empty;
            this.CookingDirections = new ObservableCollection<string>();
            this.MealIngredients = new ObservableCollection<string>();
        }
    }
}
