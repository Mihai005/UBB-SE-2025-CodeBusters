namespace MealPlannerProject.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using MealPlannerProject.Models;
    using MealPlannerProject.Pages;
    using MealPlannerProject.Services;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Microsoft.UI;
    using Microsoft.UI.Xaml.Media;
    using Windows.UI;

    public class AddFoodPageViewModel : ViewModelBase
    {
        private string searchText;
        private object selectedItem;
        private bool isSearchVisible;
        private string selectedUnit = "g";
        private string connectionString;

        public ObservableCollection<object> SearchResults { get; set; } = new ObservableCollection<object>();

        private string totalProtein;
        private string goalProtein;
        private string remainingProtein;

        private string totalCarbohydrates;
        private string goalCarbohydrates;
        private string remainingCarbohydrates;

        private string totalFiber;
        private string goalFiber;
        private string remainingFiber;

        private string totalFat;
        private string goalFat;
        private string remainingFat;

        private string totalSugar;
        private string goalSugar;
        private string remainingSugar;

        private void SetProperty(ref string field, string value, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            if (field != value)
            {
                field = value;
                this.OnPropertyChanged(propertyName);
            }
        }

        public string TotalProtein { get => this.totalProtein; set => this.SetProperty(ref this.totalProtein, value); }

        public string GoalProtein { get => this.goalProtein; set => this.SetProperty(ref this.goalProtein, value); }

        public string RemainingProtein { get => this.remainingProtein; set => this.SetProperty(ref this.remainingProtein, value); }

        public string TotalCarbohydrates { get => this.totalCarbohydrates; set => this.SetProperty(ref this.totalCarbohydrates, value); }

        public string GoalCarbohydrates { get => this.goalCarbohydrates; set => this.SetProperty(ref this.goalCarbohydrates, value); }

        public string RemainingCarbohydrates { get => this.remainingCarbohydrates; set => this.SetProperty(ref this.remainingCarbohydrates, value); }

        public string TotalFiber { get => this.totalFiber; set => this.SetProperty(ref this.totalFiber, value); }

        public string GoalFiber { get => this.goalFiber; set => this.SetProperty(ref this.goalFiber, value); }

        public string RemainingFiber { get => this.remainingFiber; set => this.SetProperty(ref this.remainingFiber, value); }

        public string TotalFat { get => this.totalFat; set => this.SetProperty(ref this.totalFat, value); }

        public string GoalFat { get => this.goalFat; set => this.SetProperty(ref this.goalFat, value); }

        public string RemainingFat { get => this.remainingFat; set => this.SetProperty(ref this.remainingFat, value); }

        public string TotalSugar { get => this.totalSugar; set => this.SetProperty(ref this.totalSugar, value); }

        public string GoalSugar { get => this.goalSugar; set => this.SetProperty(ref this.goalSugar, value); }

        public string RemainingSugar { get => this.remainingSugar; set => this.SetProperty(ref this.remainingSugar, value); }

        private static int userId;

        public static int UserId { get => userId; set => userId = value; }

        private readonly MacrosService macrosService;

        // FOR BACK COMMAND <-
        public AddFoodPageViewModel()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
            string? localDataSource = config["LocalDataSource"];
            string? initialCatalog = config["InitialCatalog"];
            this.connectionString = $"Data Source={localDataSource};Initial Catalog={initialCatalog};Integrated Security=True; TrustServerCertificate = True";
            this.BackCommand = new RelayCommand(this.GoBack);
            this.NextCommand = new RelayCommand(this.GoNext);
            this.AddToMealCommand = new RelayCommand(this.AddToMeal, this.CanAddToMeal);
            this.FetchServingUnits();

            Console.WriteLine("AddFoodPageViewModel initialized");
            Console.WriteLine($"AddToMealCommand is null: {this.AddToMealCommand == null}");

            int number_userId = userId;

            // Initialize MacrosService
            this.macrosService = new MacrosService();
            // Initialize macros values from database
            this.TotalProtein = this.macrosService.GetProteinIntake(number_userId).ToString();
            this.TotalCarbohydrates = this.macrosService.GetCarbohydratesIntake(number_userId).ToString();
            this.TotalFat = this.macrosService.GetFatIntake(number_userId).ToString();
            this.TotalFiber = this.macrosService.GetFiberIntake(number_userId).ToString();
            this.TotalSugar = this.macrosService.GetSugarIntake(number_userId).ToString();

            // Initialize values
            this.GoalProtein = "30";
            this.GoalCarbohydrates = "200";
            this.GoalFiber = "30";
            this.GoalFat = "90";
            this.GoalSugar = "50";

            this.RemainingProtein = (float.Parse(this.GoalProtein) - float.Parse(this.TotalProtein)).ToString();
            this.RemainingCarbohydrates = (float.Parse(this.GoalCarbohydrates) - float.Parse(this.TotalCarbohydrates)).ToString();
            this.RemainingFiber = (float.Parse(this.GoalFiber) - float.Parse(this.TotalFiber)).ToString();
            this.RemainingFat = (float.Parse(this.GoalFat) - float.Parse(this.TotalFat)).ToString();
            this.RemainingSugar = (float.Parse(this.GoalSugar) - float.Parse(this.TotalSugar)).ToString();
        }

        public ICommand NextCommand { get; }

        public ICommand BackCommand { get; }

        private void GoBack()
        {
            NavigationService.Instance.GoBack();
        }

        private void GoNext()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage));
        }

        // for SEARCH BAR INGREDIENT / MEAL
        public ICommand AddToMealCommand { get; }

        public string SearchText
        {
            get => this.searchText;
            set
            {
                this.searchText = value;
                this.OnPropertyChanged();
                this.PerformSearch();
            }
        }

        public object? SelectedItem
        {
            get => this.selectedItem;
            set
            {
                this.selectedItem = value;
                this.OnPropertyChanged();
                ((RelayCommand)this.AddToMealCommand)?.RaiseCanExecuteChanged();
                Console.WriteLine($"SelectedItem changed to: {this.selectedItem}");
            }
        }

        public bool IsSearchVisible
        {
            get => this.isSearchVisible;
            set
            {
                this.isSearchVisible = value;
                this.OnPropertyChanged();
            }
        }

        private void PerformSearch()
        {
            this.SearchResults.Clear();
            if (string.IsNullOrWhiteSpace(this.SearchText))
            {
                this.IsSearchVisible = false;
                return;
            }

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                // Search Meals first
                string mealQuery = "SELECT m_id, m_name FROM meals WHERE m_name LIKE @search + '%'";
                using (SqlCommand cmd = new SqlCommand(mealQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@search", this.SearchText);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.SearchResults.Add(new { Id = reader["m_id"], Name = reader["m_name"], Type = "Meal" });
                        }
                    }
                }

                // If no meals found, search Ingredients
                if (this.SearchResults.Count == 0)
                {
                    string ingredientQuery = "SELECT i_id, i_name FROM ingredients WHERE i_name LIKE @search + '%'";
                    using (SqlCommand cmd = new SqlCommand(ingredientQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@search", this.SearchText);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.SearchResults.Add(new { Id = reader["i_id"], Name = reader["i_name"], Type = "Ingredient" });
                            }
                        }
                    }
                }
            }

            this.IsSearchVisible = this.SearchResults.Count > 0;
        }

        private bool CanAddToMeal()
        {
            bool canAdd = this.SelectedItem != null && this.servingsCount > 0;
            Console.WriteLine($"CanAddToMeal: {canAdd}, SelectedItem: {this.SelectedItem != null}, ServingsCount: {this.servingsCount}");
            return canAdd;
        }

        private void AddToMeal()
        {
            Console.WriteLine("AddToMeal method called");

            if (this.SelectedItem == null)
            {
                Console.WriteLine("SelectedItem is null.");
                return;
            }

            try
            {
                int selectedId = (int)this.SelectedItem.GetType().GetProperty("Id")?.GetValue(this.SelectedItem);
                string type = (string)this.SelectedItem.GetType().GetProperty("Type")?.GetValue(this.SelectedItem);

                Console.WriteLine($"Selected Item ID: {selectedId}");
                Console.WriteLine($"Selected Item Type: {type}");
                Console.WriteLine($"Servings Count: {this.servingsCount}");
                Console.WriteLine($"Unit Name: {this.SelectedUnit}");

                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("Database connection established.");

                        string insertQuery = @"
                                        INSERT INTO daily_meals (m_id, date_eaten, servings, unit_name, total_calories, 
                                    total_protein, total_carbohydrates, total_fat, total_fiber, total_sugar)
                                        SELECT 
                                            @mealId,  -- <-- Update parameter here
                                            GETDATE(),  
                                            @servings, 
                                            @unit_name, 
                                            m.calories * @servings AS total_calories, 
                                            m.protein * @servings AS total_protein, 
                                            m.carbohydrates * @servings AS total_carbohydrates, 
                                            m.fat * @servings AS total_fat, 
                                            m.fiber * @servings AS total_fiber, 
                                            m.sugar * @servings AS total_sugar
                                        FROM meals m
                                        WHERE m.m_id = @mealId";  // <-- Update parameter here as well

                        using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@mealId", selectedId);
                            cmd.Parameters.AddWithValue("@unit_name", SelectedUnit);
                            cmd.Parameters.AddWithValue("@servings", servingsCount);

                            Console.WriteLine("Executing SQL command...");
                            int result = cmd.ExecuteNonQuery();
                            Console.WriteLine($"Rows affected: {result}");

                            if (result > 0)
                            {
                                Console.WriteLine("Food successfully added to meal!");
                            }
                            else
                            {
                                Console.WriteLine("No rows were affected. The meal might not exist in the database.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error inserting data: " + ex.Message);
                        Console.WriteLine("Stack trace: " + ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing selected item: " + ex.Message);
                Console.WriteLine("Stack trace: " + ex.StackTrace);
            }

            // Reset selection
            this.SelectedItem = null;
            this.SearchText = string.Empty;
            this.IsSearchVisible = false;
        }





        // FOR SELECTED MEAL TYPE


        private SolidColorBrush breakfastColor = new SolidColorBrush(Colors.Transparent);
        private SolidColorBrush lunchColor = new SolidColorBrush(Colors.Transparent);
        private SolidColorBrush dinnerColor = new SolidColorBrush(Colors.Transparent);
        private SolidColorBrush snackColor = new SolidColorBrush(Colors.Transparent);

        public SolidColorBrush BreakfastColor
        {
            get => this.breakfastColor;
            set
            {
                this.breakfastColor = value;
                this.OnPropertyChanged();
            }
        }

        public SolidColorBrush LunchColor
        {
            get => this.lunchColor;
            set
            {
                this.lunchColor = value;
                this.OnPropertyChanged();
            }
        }

        public SolidColorBrush DinnerColor
        {
            get => this.dinnerColor;
            set
            {
                this.dinnerColor = value;
                this.OnPropertyChanged();
            }
        }

        public SolidColorBrush SnackColor
        {
            get => this.snackColor;
            set
            {
                this.snackColor = value;
                this.OnPropertyChanged();
            }
        }

        public void SetCategoryHighlight(string category)
        {
            // Default colors
            this.BreakfastColor = new SolidColorBrush(Color.FromArgb(255, 199, 110, 78)); // Reddish brown
            this.LunchColor = new SolidColorBrush(Color.FromArgb(255, 91, 119, 105)); // Greenish grey
            this.DinnerColor = new SolidColorBrush(Color.FromArgb(255, 181, 136, 94)); // Light brown
            this.SnackColor = new SolidColorBrush(Color.FromArgb(255, 19, 50, 36)); // Dark green

            // Highlight selected category
            SolidColorBrush highlightColor = new SolidColorBrush(Color.FromArgb(255, 227, 199, 177)); // Beige



            switch (category)
            {
                case "Breakfast": this.BreakfastColor = highlightColor; break;
                case "Lunch": this.LunchColor = highlightColor; break;
                case "Dinner": this.DinnerColor = highlightColor; break;
                case "Snack": this.SnackColor = highlightColor; break;
            }
        }


        // SERVING UNITS

        public ObservableCollection<ServingUnitModel> ServingUnits { get; set; } = new ObservableCollection<ServingUnitModel>();

        private void FetchServingUnits()
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                string query = "SELECT unit_name FROM serving_units";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var servingUnits = new ObservableCollection<ServingUnitModel>();
                        while (reader.Read())
                        {
                            servingUnits.Add(new ServingUnitModel
                            {
                                UnitName = reader["unit_name"].ToString(),
                            });
                        }

                        this.ServingUnits = servingUnits;
                    }
                }
            }
        }

        public string SelectedUnit
        {
            get => this.selectedUnit;
            set
            {
                if (this.selectedUnit != value)
                {
                    this.selectedUnit = value;
                    this.OnPropertyChanged(nameof(this.SelectedUnit));
                }
            }
        }

        private int servingsCount = 0;

        public string ServingsCount
        {
            get => $"{this.servingsCount} servings";
            set
            {
                // Extract just the number from the string if it contains "servings"
                string numericValue = value.Replace("servings", "").Trim();
                if (int.TryParse(numericValue, out int newValue))
                {
                    this.servingsCount = newValue;
                    this.OnPropertyChanged();
                    ((RelayCommand)this.AddToMealCommand)?.RaiseCanExecuteChanged();
                    Console.WriteLine($"ServingsCount updated to: {this.servingsCount}");
                }
                else
                {
                    Console.WriteLine($"Failed to parse servings value: '{value}'");
                }
            }
        }
    }
}