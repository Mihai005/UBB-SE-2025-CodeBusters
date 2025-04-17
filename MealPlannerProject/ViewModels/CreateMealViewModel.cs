namespace MealPlannerProject.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.Input;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.Models;
    using MealPlannerProject.Services;
    using Microsoft.UI.Xaml.Controls;
    using Windows.Storage;

    public class CreateMealViewModel : ViewModelBase
    {
        private readonly IMealService mealService;
        private string mealName = string.Empty; // Initialize to avoid null
        private string cookingTime = string.Empty; // Initialize to avoid null
        private string selectedMealType = string.Empty; // Initialize to avoid null
        private string selectedCookingLevel = string.Empty; // Initialize to avoid null
        private ObservableCollection<string> directions;
        private ObservableCollection<MealIngredient> ingredients;
        private StorageFile selectedImage = null!; // Use null-forgiving operator

        // Calculated macros
        private int calories;
        private int protein;
        private int carbs;
        private int fats;
        private int fiber;
        private int sugar;

        public CreateMealViewModel()
        {
            this.mealService = new MealService();

            // Initialize collections
            this.directions = new ObservableCollection<string>();
            this.ingredients = new ObservableCollection<MealIngredient>();

            // Initialize commands
            this.GoBackCommand = new RelayCommand(this.OnGoBack);
            this.AddDirectionCommand = new RelayCommand(this.OnAddDirection);
            this.AddIngredientCommand = new RelayCommand(this.OnAddIngredient);
            this.SelectMealTypeCommand = new RelayCommand<string?>(this.OnSelectMealType); // Fixes SA1101
            this.SelectCookingLevelCommand = new RelayCommand<string?>(this.OnSelectCookingLevel); // Fixes SA1101
        }

        public string MealName
        {
            get => this.mealName; // Fixes SA1101
            set
            {
                if (this.mealName != value) // Fixes SA1101
                {
                    this.mealName = value; // Fixes SA1101
                    this.OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public string CookingTime
        {
            get => this.cookingTime; // Fixes SA1101
            set
            {
                if (this.cookingTime != value) // Fixes SA1101
                {
                    this.cookingTime = value; // Fixes SA1101
                    this.OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public string SelectedMealType
        {
            get => this.selectedMealType; // Fixes SA1101
            set
            {
                if (this.selectedMealType != value) // Fixes SA1101
                {
                    this.selectedMealType = value; // Fixes SA1101
                    this.OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public string SelectedCookingLevel
        {
            get => this.selectedCookingLevel; // Fixes SA1101
            set
            {
                if (this.selectedCookingLevel != value) // Fixes SA1101
                {
                    this.selectedCookingLevel = value; // Fixes SA1101
                    this.OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public StorageFile SelectedImage
        {
            get => this.selectedImage; // Fixes SA1101
            set
            {
                if (this.selectedImage != value) // Fixes SA1101
                {
                    this.selectedImage = value; // Fixes SA1101
                    this.OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public ObservableCollection<string> Directions
        {
            get => this.directions; // Fixes SA1101
            set
            {
                if (this.directions != value) // Fixes SA1101
                {
                    this.directions = value; // Fixes SA1101
                    this.OnPropertyChanged(); // Fixes SA1101
                }
            }
        }

        public ObservableCollection<string> IngredientNames
        {
            get => new(this.ingredients.Select(i => $"{i.IngredientName}: {i.Quantity} servings")); // Fixes SA1101
        }

        public ObservableCollection<MealIngredient> Ingredients
        {
            get => this.ingredients; // Fixes SA1101
            set
            {
                if (this.ingredients != value) // Fixes SA1101
                {
                    this.ingredients = value; // Fixes SA1101
                    this.OnPropertyChanged(nameof(this.IngredientNames)); // Fixes SA1101
                    this.CalculateTotalMacros(); // Fixes SA1101
                }
            }
        }

        public ICommand GoBackCommand { get; }

        public ICommand AddDirectionCommand { get; }

        public ICommand AddIngredientCommand { get; }

        public ICommand SelectMealTypeCommand { get; }

        public ICommand SelectCookingLevelCommand { get; }


        private void CalculateTotalMacros()
        {
            var calculatedIngredients = this.ingredients.Select(i => i.CalculateMacros()); // Fixes SA1101

            this.calories = (int)calculatedIngredients.Sum(i => i.Calories); // Fixes SA1101
            this.protein = (int)calculatedIngredients.Sum(i => i.Protein); // Fixes SA1101
            this.carbs = (int)calculatedIngredients.Sum(i => i.Carbs); // Fixes SA1101
            this.fats = (int)calculatedIngredients.Sum(i => i.Fats); // Fixes SA1101
            this.fiber = (int)calculatedIngredients.Sum(i => i.Fiber); // Fixes SA1101
            this.sugar = (int)calculatedIngredients.Sum(i => i.Sugar); // Fixes SA1101

            this.OnPropertyChanged(nameof(this.TotalCalories)); // Fixes SA1101
            this.OnPropertyChanged(nameof(this.TotalProtein)); // Fixes SA1101
            this.OnPropertyChanged(nameof(this.TotalCarbs)); // Fixes SA1101
            this.OnPropertyChanged(nameof(this.TotalFats)); // Fixes SA1101
            this.OnPropertyChanged(nameof(this.TotalFiber)); // Fixes SA1101
            this.OnPropertyChanged(nameof(this.TotalSugar)); // Fixes SA1101
        }

        public int TotalCalories => this.calories; // Fixes SA1101

        public int TotalProtein => this.protein; // Fixes SA1101

        public int TotalCarbs => this.carbs; // Fixes SA1101

        public int TotalFats => this.fats; // Fixes SA1101

        public int TotalFiber => this.fiber; // Fixes SA1101

        public int TotalSugar => this.sugar; // Fixes SA1101

        public async Task<bool> CreateMealAsync(Meal meal)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.selectedCookingLevel)) // Fixes SA1101
                {
                    this.selectedCookingLevel = "Beginner"; // Fixes SA1101
                }

                // Convert cooking time to integer
                if (!int.TryParse(this.cookingTime, out int cookingTimeMinutes)) // Fixes SA1101
                {
                    cookingTimeMinutes = 0;
                }

                // Set all meal properties including calculated macros
                meal.Name = this.mealName; // Fixes SA1101
                meal.PreparationTime = cookingTimeMinutes;
                meal.CookingLevel = this.selectedCookingLevel; // Fixes SA1101
                meal.Calories = this.calories; // Fixes SA1101
                meal.Protein = this.protein; // Fixes SA1101
                meal.Carbohydrates = this.carbs; // Fixes SA1101
                meal.Fat = this.fats; // Fixes SA1101
                meal.Fiber = this.fiber; // Fixes SA1101
                meal.Sugar = this.sugar; // Fixes SA1101

                // Create the meal first
                int mealId = await this.mealService.CreateMealAsync(meal); // Fixes SA1101
                if (mealId > 0)
                {
                    // Then add the meal-ingredient relationships
                    foreach (var ingredient in this.ingredients) // Fixes SA1101
                    {
                        await this.mealService.AddIngredientToMealAsync(mealId, ingredient.IngredientId, ingredient.Quantity); // Fixes SA1101
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating meal: {ex.Message}");
                return false;
            }
        }

        private void OnGoBack()
        {
            NavigationService.Instance.GoBack();
        }

        private async void OnAddDirection()
        {
            var dialog = new TextBox
            {
                PlaceholderText = "Enter direction step",
            };

            ContentDialog contentDialog = new ContentDialog
            {
                Title = "Add Direction",
                Content = dialog,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                XamlRoot = App.MainWindow.Content.XamlRoot,
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if (!string.IsNullOrWhiteSpace(dialog.Text))
                {
                    this.Directions.Add($"{this.Directions.Count + 1}. {dialog.Text}"); // Fixes SA1101
                }
            }
        }

        private async void OnAddIngredient()
        {
            var quantityBox = new TextBox { PlaceholderText = "Enter quantity" };
            var ingredientBox = new TextBox { PlaceholderText = "Enter ingredient name" };

            var panel = new StackPanel();
            panel.Children.Add(ingredientBox);
            panel.Children.Add(quantityBox);

            ContentDialog contentDialog = new ContentDialog
            {
                Title = "Add Ingredient",
                Content = panel,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                XamlRoot = App.MainWindow.Content.XamlRoot,
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                if (!string.IsNullOrWhiteSpace(ingredientBox.Text) && !string.IsNullOrWhiteSpace(quantityBox.Text))
                {
                    if (float.TryParse(quantityBox.Text, out float quantity))
                    {
                        // Get ingredient from database
                        var ingredient = await this.mealService.RetrieveIngredientByNameAsync(ingredientBox.Text); // Fixes SA1101
                        if (ingredient != Ingredient.NoIngredient)
                        {
                            var mealIngredient = new MealIngredient
                            {
                                IngredientId = ingredient.Id,
                                IngredientName = ingredient.Name,
                                Quantity = quantity,
                                Protein = ingredient.Protein,
                                Calories = ingredient.Calories,
                                Carbs = ingredient.Carbs,
                                Fats = ingredient.Fats,
                                Fiber = ingredient.Fiber,
                                Sugar = ingredient.Sugar,
                            };

                            this.ingredients.Add(mealIngredient); // Fixes SA1101
                            this.OnPropertyChanged(nameof(this.IngredientNames)); // Fixes SA1101
                            this.CalculateTotalMacros(); // Fixes SA1101
                        }
                        else
                        {
                            // Show error that ingredient wasn't found
                            var errorDialog = new ContentDialog
                            {
                                Title = "Error",
                                Content = "Ingredient not found in database",
                                CloseButtonText = "OK",
                                XamlRoot = App.MainWindow.Content.XamlRoot,
                            };
                            await errorDialog.ShowAsync();
                        }
                    }
                }
            }
        }

        private void OnSelectMealType(string? mealType) // Fixes CS8622
        {
            this.SelectedMealType = mealType ?? string.Empty; // Fixes SA1101 and ensures null safety
        }

        private void OnSelectCookingLevel(string? level) // Fixes CS8622
        {
            this.SelectedCookingLevel = level ?? string.Empty; // Fixes SA1101 and ensures null safety
        }
    }
}
