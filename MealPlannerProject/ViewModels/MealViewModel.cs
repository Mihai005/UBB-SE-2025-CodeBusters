namespace MealPlannerProject.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using MealPlannerProject.Models;

    public class MealViewModel : INotifyPropertyChanged
    {
        private readonly MealCreate model;

        public string MealName
        {
            get => this.model.MealName;
            set
            {
                if (this.model.MealName != value)
                {
                    this.model.MealName = value;
                    Debug.WriteLine($"MealName set to: {value}");
                    this.OnPropertyChanged(nameof(this.model.MealName));
                }
            }
        }

        public string CookingTime
        {
            get => this.model.CookingTime;
            set
            {
                if (this.model.CookingTime != value)
                {
                    this.model.CookingTime = value;
                    Debug.WriteLine($"CookingTime set to: {value}");
                    this.OnPropertyChanged(nameof(this.model.CookingTime));
                }
            }
        }

        public ObservableCollection<string> CookingDirections
        {
            get => this.model.CookingDirections;
            set
            {
                if (this.model.CookingDirections != value)
                {
                    this.model.CookingDirections = value;
                    Debug.WriteLine($"Directions set with {value?.Count ?? 0} items");
                    this.OnPropertyChanged(nameof(this.model.CookingDirections));
                }
            }
        }

        public ObservableCollection<string> MealIngredients
        {
            get => this.model.MealIngredients;
            set
            {
                if (this.model.MealIngredients != value)
                {
                    this.model.MealIngredients = value;
                    Debug.WriteLine($"Ingredients set with {value?.Count ?? 0} items");
                    this.OnPropertyChanged(nameof(this.model.MealIngredients));
                }
            }
        }

        public int CalorieCount
        {
            get => this.model.CalorieCount;
            set
            {
                if (this.model.CalorieCount != value)
                {
                    this.model.CalorieCount = value;
                    Debug.WriteLine($"Calories set to: {value}");
                    this.OnPropertyChanged(nameof(this.model.CalorieCount));
                }
            }
        }

        public int ProteinGrams
        {
            get => this.model.ProteinGrams;
            set
            {
                if (this.model.ProteinGrams != value)
                {
                    this.model.ProteinGrams = value;
                    Debug.WriteLine($"Protein set to: {value}");
                    this.OnPropertyChanged(nameof(this.model.ProteinGrams));
                }
            }
        }

        public int CarbohydrateGrams
        {
            get => this.model.CarbohydrateGrams;
            set
            {
                if (this.model.CarbohydrateGrams != value)
                {
                    this.model.CarbohydrateGrams = value;
                    Debug.WriteLine($"Carbs set to: {value}");
                    this.OnPropertyChanged(nameof(this.model.CarbohydrateGrams));
                }
            }
        }

        public int FatGrams
        {
            get => this.model.FatGrams;
            set
            {
                if (this.model.FatGrams != value)
                {
                    this.model.FatGrams = value;
                    Debug.WriteLine($"Fat set to: {value}");
                    this.OnPropertyChanged(nameof(this.model.FatGrams));
                }
            }
        }

        public int FiberGrams
        {
            get => this.model.FiberGrams;
            set
            {
                if (this.model.FiberGrams != value)
                {
                    this.model.FiberGrams = value;
                    Debug.WriteLine($"Fiber set to: {value}");
                    this.OnPropertyChanged(nameof(this.model.FiberGrams));
                }
            }
        }

        public int SugarGrams
        {
            get => this.model.SugarGrams;
            set
            {
                if (this.model.SugarGrams != value)
                {
                    this.model.SugarGrams = value;
                    Debug.WriteLine($"Sugar set to: {value}");
                    this.OnPropertyChanged(nameof(this.model.SugarGrams));
                }
            }
        }

        public MealViewModel()
        {
            this.model = new MealCreate();
            Debug.WriteLine("Initializing MealViewModel");
        }

        public void InitializeFromMeal(Meal meal)
        {
            Debug.WriteLine($"Initializing MealViewModel from Meal: {meal?.Name ?? "null"}");

            if (meal == null)
            {
                Debug.WriteLine("Warning: Meal object is null");
                return;
            }

            this.MealName = meal.Name;
            this.CookingTime = $"{meal.PreparationTime} min";
            this.CalorieCount = meal.Calories;
            this.ProteinGrams = meal.Protein;
            this.CarbohydrateGrams = meal.Carbohydrates;
            this.FatGrams = meal.Fat;
            this.FiberGrams = meal.Fiber;
            this.SugarGrams = meal.Sugar;

            if (!string.IsNullOrEmpty(meal.Ingredients))
            {
                this.MealIngredients = new ObservableCollection<string>(meal.Ingredients.Split('\n'));
                Debug.WriteLine($"Loaded {this.MealIngredients.Count} ingredients");
            }
            else
            {
                Debug.WriteLine("No ingredients found in meal");
                this.MealIngredients = new ObservableCollection<string> { "No ingredients available" };
            }

            if (!string.IsNullOrEmpty(meal.Recipe))
            {
                this.CookingDirections = new ObservableCollection<string>(meal.Recipe.Split('\n'));
                Debug.WriteLine($"Loaded {this.CookingDirections.Count} directions");
            }
            else
            {
                Debug.WriteLine("No recipe found in meal");
                this.CookingDirections = new ObservableCollection<string> { "No directions available" };
            }

            Debug.WriteLine("Finished initializing MealViewModel from Meal");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
