namespace MealPlannerProject.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows.Input;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.Pages;
    using MealPlannerProject.Queries;
    using MealPlannerProject.Services;

    public class MainViewModel : INotifyPropertyChanged
    {
        // Generic method to update properties
        private string circleText;
        private string baseGoalCalories;
        private string foodCalories;
        private string exerciseCalories;
        private string waterPercentage;
        private string waterGoal;

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

        public string CircleText { get => this.circleText; set => this.SetProperty(ref this.circleText, value); }

        public string BaseGoal_Calories { get => this.baseGoalCalories; set => this.SetProperty(ref this.baseGoalCalories, value); }

        public string Food_Calories { get => this.foodCalories; set => this.SetProperty(ref this.foodCalories, value); }

        public string Exercise_Calories { get => this.exerciseCalories; set => this.SetProperty(ref this.exerciseCalories, value); }

        public string Total_Protein { get => this.totalProtein; set => this.SetProperty(ref this.totalProtein, value); }

        public string Goal_Protein { get => this.goalProtein; set => this.SetProperty(ref this.goalProtein, value); }

        public string Remaining_Protein { get => this.remainingProtein; set => this.SetProperty(ref this.remainingProtein, value); }

        public string Total_Carbohydrates { get => this.totalCarbohydrates; set => this.SetProperty(ref this.totalCarbohydrates, value); }

        public string Goal_Carbohydrates { get => this.goalCarbohydrates; set => this.SetProperty(ref this.goalCarbohydrates, value); }

        public string Remaining_Carbohydrates { get => this.remainingCarbohydrates; set => this.SetProperty(ref this.remainingCarbohydrates, value); }

        public string Total_Fiber { get => this.totalFiber; set => this.SetProperty(ref this.totalFiber, value); }

        public string Goal_Fiber { get => this.goalFiber; set => this.SetProperty(ref this.goalFiber, value); }

        public string Remaining_Fiber { get => this.remainingFiber; set => this.SetProperty(ref this.remainingFiber, value); }

        public string Total_Fat { get => this.totalFat; set => this.SetProperty(ref this.totalFat, value); }

        public string Goal_Fat { get => this.goalFat; set => this.SetProperty(ref this.goalFat, value); }

        public string Remaining_Fat { get => this.remainingFat; set => this.SetProperty(ref this.remainingFat, value); }

        public string Total_Sugar { get => this.totalSugar; set => this.SetProperty(ref this.totalSugar, value); }

        public string Goal_Sugar { get => this.goalSugar; set => this.SetProperty(ref this.goalSugar, value); }

        public string Remaining_Sugar { get => this.remainingSugar; set => this.SetProperty(ref this.remainingSugar, value); }

        public string Water_Percentage { get => this.waterPercentage; set => this.SetProperty(ref this.waterPercentage, value); }

        public string Water_Goal { get => this.waterGoal; set => this.SetProperty(ref this.waterGoal, value); }

        // Define properties for R1 to R6
        private string[] rValues = new string[36];

        public string R1 { get => this.rValues[0]; set => this.SetProperty(ref this.rValues[0], value); }

        public string R1Calories { get => this.rValues[1]; set => this.SetProperty(ref this.rValues[1], value); }

        public string R1Diet { get => this.rValues[2]; set => this.SetProperty(ref this.rValues[2], value); }

        public string R1Level { get => this.rValues[3]; set => this.SetProperty(ref this.rValues[3], value); }

        public string R1Time { get => this.rValues[4]; set => this.SetProperty(ref this.rValues[4], value); }

        public string R1MealType { get => this.rValues[5]; set => this.SetProperty(ref this.rValues[5], value); }

        public string R2 { get => this.rValues[6]; set => this.SetProperty(ref this.rValues[6], value); }

        public string R2Calories { get => this.rValues[7]; set => this.SetProperty(ref this.rValues[7], value); }

        public string R2Diet { get => this.rValues[8]; set => this.SetProperty(ref this.rValues[8], value); }

        public string R2Level { get => this.rValues[9]; set => this.SetProperty(ref this.rValues[9], value); }

        public string R2Time { get => this.rValues[10]; set => this.SetProperty(ref this.rValues[10], value); }

        public string R2MealType { get => this.rValues[11]; set => this.SetProperty(ref this.rValues[11], value); }

        public string R3 { get => this.rValues[12]; set => this.SetProperty(ref this.rValues[12], value); }

        public string R3Calories { get => this.rValues[13]; set => this.SetProperty(ref this.rValues[13], value); }

        public string R3Diet { get => this.rValues[14]; set => this.SetProperty(ref this.rValues[14], value); }

        public string R3Level { get => this.rValues[15]; set => this.SetProperty(ref this.rValues[15], value); }

        public string R3Time { get => this.rValues[16]; set => this.SetProperty(ref this.rValues[16], value); }

        public string R3MealType { get => this.rValues[17]; set => this.SetProperty(ref this.rValues[17], value); }

        public string R4 { get => this.rValues[18]; set => this.SetProperty(ref this.rValues[18], value); }

        public string R4Calories { get => this.rValues[19]; set => this.SetProperty(ref this.rValues[19], value); }

        public string R4Diet { get => this.rValues[20]; set => this.SetProperty(ref this.rValues[20], value); }

        public string R4Level { get => this.rValues[21]; set => this.SetProperty(ref this.rValues[21], value); }

        public string R4Time { get => this.rValues[22]; set => this.SetProperty(ref this.rValues[22], value); }

        public string R4MealType { get => this.rValues[23]; set => this.SetProperty(ref this.rValues[23], value); }

        public string R5 { get => this.rValues[24]; set => this.SetProperty(ref this.rValues[24], value); }

        public string R5Calories { get => this.rValues[25]; set => this.SetProperty(ref this.rValues[25], value); }

        public string R5Diet { get => this.rValues[26]; set => this.SetProperty(ref this.rValues[26], value); }

        public string R5Level { get => this.rValues[27]; set => this.SetProperty(ref this.rValues[27], value); }

        public string R5Time { get => this.rValues[28]; set => this.SetProperty(ref this.rValues[28], value); }

        public string R5MealType { get => this.rValues[29]; set => this.SetProperty(ref this.rValues[29], value); }


        public string R6 { get => this.rValues[30]; set => this.SetProperty(ref this.rValues[30], value); }

        public string R6Calories { get => this.rValues[31]; set => this.SetProperty(ref this.rValues[31], value); }

        public string R6Diet { get => this.rValues[32]; set => this.SetProperty(ref this.rValues[32], value); }

        public string R6Level { get => this.rValues[33]; set => this.SetProperty(ref this.rValues[33], value); }

        public string R6Time { get => this.rValues[34]; set => this.SetProperty(ref this.rValues[34], value); }

        public string R6MealType { get => this.rValues[35]; set => this.SetProperty(ref this.rValues[35], value); }

        public ICommand RefreshCommand { get; }

        // Generic property setter method
        private void SetProperty(ref string field, string value, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            if (field != value)
            {
                field = value;
                this.OnPropertyChanged(propertyName);
            }
        }

        // Commands -------------------------------------

        public ICommand SocialMedia { get; }

        public ICommand RefreshCircleTextCommand { get; }

        public ICommand RefreshBaseGoalTextCommand { get; }

        public ICommand RefreshFoodTextCommand { get; }

        public ICommand RefreshExerciseTextCommand { get; }


        public ICommand RefreshTotalPrTextCommand { get; }

        public ICommand RefreshGoalPrTextCommand { get; }

        public ICommand RefreshLeftPrTextCommand { get; }


        public ICommand RefreshTotalCarbTextCommand { get; }

        public ICommand RefreshGoalCarbTextCommand { get; }

        public ICommand RefreshLeftCarbTextCommand { get; }

        public ICommand RefreshTotalFibTextCommand { get; }

        public ICommand RefreshGoalFibTextCommand { get; }

        public ICommand RefreshLeftFibTextCommand { get; }


        public ICommand RefreshTotalFatTextCommand { get; }

        public ICommand RefreshGoalFatTextCommand { get; }

        public ICommand RefreshLeftFatTextCommand { get; }

        public ICommand RefreshTotalSugTextCommand { get; }

        public ICommand RefreshGoalSugTextCommand { get; }

        public ICommand RefreshLeftSugTextCommand { get; }

        public ICommand NavigateCommand { get; }

        public ICommand DisplayMeals { get; }

        public ICommand CreateMeal { get; }

        public ICommand GroceryList { get; }

        public ICommand AddBreakfast { get; }

        public ICommand AddLunch { get; }

        public ICommand AddDinner { get; }

        public ICommand AddSnack { get; }

        public ICommand AddWater300Command { get; }

        public ICommand AddWater400Command { get; }

        public ICommand AddWater500Command { get; }

        public ICommand AddWater750Command { get; }

        public ICommand RemoveWater300Command { get; }

        public ICommand RemoveWater400Command { get; }

        public ICommand RemoveWater500Command { get; }

        public ICommand RemoveWater750Command { get; }

        // ----------------------------------------

        private readonly IWaterIntakeService waterService;
        private readonly CalorieService calorieService;
        private readonly MacrosService macrosService;

        public static int UserId { get; set; }

        public ICommand RefreshMealsCommand { get; }

        [Obsolete]
        public MainViewModel()
        {

            int number_userId = UserId;

            // Initialize WaterService
            this.waterService = new WaterIntakeService();
            this.waterService.AddUserIfNotExists(number_userId); // Ensure user exists in the water tracker table

            // Initialize CalorieService
            this.calorieService = new CalorieService();

            // Initialize MacrosService
            this.macrosService = new MacrosService();

            System.Diagnostics.Debug.WriteLine("Getting water intake...");

            // Initialize water intake from database
            this.WaterIntake = this.waterService.GetWaterIntake(number_userId).ToString();

            // Initialize water commands
            this.AddWater300Command = new RelayCommand(() => this.UpdateWaterIntake(300));
            this.AddWater400Command = new RelayCommand(() => this.UpdateWaterIntake(400));
            this.AddWater500Command = new RelayCommand(() => this.UpdateWaterIntake(500));
            this.AddWater750Command = new RelayCommand(() => this.UpdateWaterIntake(750));
            this.RemoveWater300Command = new RelayCommand(() => this.RemoveWaterIntake(300));
            this.RemoveWater400Command = new RelayCommand(() => this.RemoveWaterIntake(400));
            this.RemoveWater500Command = new RelayCommand(() => this.RemoveWaterIntake(500));
            this.RemoveWater750Command = new RelayCommand(() => this.RemoveWaterIntake(750));

            System.Diagnostics.Debug.WriteLine("Loading meals...");

            // Load last 6 meals
            this.LoadLastMeals(number_userId);

            System.Diagnostics.Debug.WriteLine("Getting food calories...");
            // Initialize calorie values from database
            this.Food_Calories = this.calorieService.GetFood(number_userId).ToString();

            this.BaseGoal_Calories = "2000";
            this.Exercise_Calories = "500";

            float circleTextNr = float.Parse(this.BaseGoal_Calories) - float.Parse(this.Food_Calories) + float.Parse(this.Exercise_Calories);
            this.CircleText = circleTextNr.ToString();

            System.Diagnostics.Debug.WriteLine("Getting macros...");
            // Initialize macros values from database
            this.Total_Protein = this.macrosService.GetProteinIntake(number_userId).ToString();
            this.Total_Carbohydrates = this.macrosService.GetCarbohydratesIntake(number_userId).ToString();
            this.Total_Fat = this.macrosService.GetFatIntake(number_userId).ToString();
            this.Total_Fiber = this.macrosService.GetFiberIntake(number_userId).ToString();
            this.Total_Sugar = this.macrosService.GetSugarIntake(number_userId).ToString();

            // Initialize values
            this.Goal_Protein = "30";
            this.Goal_Carbohydrates = "200";
            this.Goal_Fiber = "30";
            this.Goal_Fat = "90";
            this.Goal_Sugar = "50";

            this.Remaining_Protein = (float.Parse(this.Goal_Protein) - float.Parse(this.Total_Protein)).ToString();
            this.Remaining_Carbohydrates = (float.Parse(this.Goal_Carbohydrates) - float.Parse(this.Total_Carbohydrates)).ToString();
            this.Remaining_Fiber = (float.Parse(this.Goal_Fiber) - float.Parse(this.Total_Fiber)).ToString();
            this.Remaining_Fat = (float.Parse(this.Goal_Fat) - float.Parse(this.Total_Fat)).ToString();
            this.Remaining_Sugar = (float.Parse(this.Goal_Sugar) - float.Parse(this.Total_Sugar)).ToString();

            this.Water_Goal = "2000";

            this.DisplayMeals = new RelayCommand(this.GoDisplayMeals);

            this.CreateMeal = new RelayCommand(this.GoCreateMeal);

            this.GroceryList = new RelayCommand(this.GoGroceryList);

            this.AddBreakfast = new RelayCommand(this.GoAddBreakfast);

            this.AddLunch = new RelayCommand(this.GoAddLunch);

            this.AddDinner = new RelayCommand(this.GoAddDinner);

            this.AddSnack = new RelayCommand(this.GoAddSnack);

            // Initialize refresh command
            // int number_userId = int.Parse(_userId);
            // RefreshMealsCommand = new RelayCommand(LoadLastMeals);

        }

        [Obsolete]
        public void LoadLastMeals(int userId)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Starting to load last meals...");

                // Initialize all slots with default values first
                string defaultRecipe = "No meals", defaultCal = "0", defaultDiet = "N/A", defaultLevel = "N/A", defaultTime = "N/A", defaultMealType = "N/A";
                this.R1 = this.R2 = this.R3 = this.R4 = this.R5 = this.R6 = defaultRecipe;
                this.R1Calories = this.R2Calories = this.R3Calories = this.R4Calories = this.R5Calories = this.R6Calories = defaultCal;
                this.R1Diet = this.R2Diet = this.R3Diet = this.R4Diet = this.R5Diet = this.R6Diet = defaultDiet;
                this.R1Level = this.R2Level = this.R3Level = this.R4Level = this.R5Level = this.R6Level = defaultLevel;
                this.R1Time = this.R2Time = this.R3Time = this.R4Time = this.R5Time = this.R6Time = defaultTime;
                this.R1MealType = this.R2MealType = this.R3MealType = this.R4MealType = this.R5MealType = this.R6MealType = defaultMealType;

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserId", userId),
                };

                DataTable mealsTable = DataLink.Instance.ExecuteReader("dbo.get_last_6_unique_meals_pr", parameters);

                if (mealsTable.Rows.Count > 0)
                {
                    for (int i = 0; i < mealsTable.Rows.Count && i < 6; i++)
                    {
                        var row = mealsTable.Rows[i];
                        string recipeName = row["RecipeName"]?.ToString() ?? "No Recipe";
                        string calories = row["Calories"]?.ToString() ?? "00";
                        string diet = row["Diet"]?.ToString() ?? "n/a";
                        string level = row["Level"]?.ToString() ?? "n/a";
                        string time = row["Time"]?.ToString() ?? "n/a";
                        string mealType = row["MealType"]?.ToString() ?? "n/a";

                        System.Diagnostics.Debug.WriteLine($"Loading meal {i + 1}: {recipeName}");

                        switch (i)
                        {
                            case 0: this.R1 = recipeName; this.R1Calories = calories; this.R1Diet = diet; this.R1Level = level; this.R1Time = time; this.R1MealType = mealType; break;
                            case 1: this.R2 = recipeName; this.R2Calories = calories; this.R2Diet = diet; this.R2Level = level; this.R2Time = time; this.R2MealType = mealType; break;
                            case 2: this.R3 = recipeName; this.R3Calories = calories; this.R3Diet = diet; this.R3Level = level; this.R3Time = time; this.R3MealType = mealType; break;
                            case 3: this.R4 = recipeName; this.R4Calories = calories; this.R4Diet = diet; this.R4Level = level; this.R4Time = time; this.R4MealType = mealType; break;
                            case 4: this.R5 = recipeName; this.R5Calories = calories; this.R5Diet = diet; this.R5Level = level; this.R5Time = time; this.R5MealType = mealType; break;
                            case 5: this.R6 = recipeName; this.R6Calories = calories; this.R6Diet = diet; this.R6Level = level; this.R6Time = time; this.R6MealType = mealType; break;
                        }
                    }
                    System.Diagnostics.Debug.WriteLine($"Loaded {mealsTable.Rows.Count} meals successfully");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading last meals: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }


        private void GoDisplayMeals()
        {
            NavigationService.Instance.NavigateTo(typeof(MealListPage));
        }

        private void GoCreateMeal()
        {
            NavigationService.Instance.NavigateTo(typeof(CreateMealPage));
        }

        private void GoGroceryList()
        {
            NavigationService.Instance.NavigateTo(typeof(GroceryListPage));
        }

        private void GoAddBreakfast()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Breakfast");
        }

        private void GoAddLunch()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Lunch");
        }

        private void GoAddDinner()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Dinner");
        }

        private void GoAddSnack()
        {
            NavigationService.Instance.NavigateTo(typeof(AddFoodPage), "Snack");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Load the initial water intake from the database
        private string waterIntake;

        public string WaterIntake
        {
            get => this.waterIntake;
            set
            {
                if (this.waterIntake != value)
                {
                    this.waterIntake = value;
                    this.OnPropertyChanged(nameof(this.WaterIntake));
                }
            }
        }

        public void UpdateWaterIntake(float amount)
        {
            if (!float.TryParse(this.WaterIntake, out float currentIntake))
            {
                currentIntake = 0; // Default to zero if parsing fails
            }

            int number_userId = UserId;
            float newIntake = currentIntake + amount;
            this.waterService.UpdateWaterIntake(number_userId, newIntake);
            this.WaterIntake = newIntake.ToString();
        }

        public void RemoveWaterIntake(float amount)
        {
            if (!float.TryParse(this.WaterIntake, out float currentIntake))
            {
                currentIntake = 0; // Default to zero if parsing fails
            }

            int number_userId = UserId;
            float newIntake = Math.Max(0, currentIntake - amount); // Ensure we don't go below 0
            this.waterService.UpdateWaterIntake(number_userId, newIntake);
            this.WaterIntake = newIntake.ToString();
        }

    }
}
