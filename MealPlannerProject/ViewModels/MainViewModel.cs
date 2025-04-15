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
        private string baseGoalCal;
        private string foodCal;
        private string exerciseCal;
        private string waterPercent;
        private string waterGoal;

        private string totalPr;
        private string goalPr;
        private string leftPr;

        private string totalCarb;
        private string goalCarb;
        private string leftCarb;

        private string totalFib;
        private string goalFib;
        private string leftFib;

        private string totalFat;
        private string goalFat;
        private string leftFat;

        private string totalSug;
        private string goalSug;
        private string leftSug;

        public string CircleText
        {
            get => this.circleText;
            set => this.SetProperty(ref this.circleText, value);
        }

        public string BaseGoal_Cal
        {
            get => this.baseGoalCal;
            set => this.SetProperty(ref this.baseGoalCal, value);
        }

        public string Food_Cal
        {
            get => this.foodCal;
            set => this.SetProperty(ref this.foodCal, value);
        }

        public string Exercise_Cal
        {
            get => this.exerciseCal;
            set => this.SetProperty(ref this.exerciseCal, value);
        }

        public string TotalPr
        {
            get => this.totalPr;
            set => this.SetProperty(ref this.totalPr, value);
        }

        public string GoalPr
        {
            get => this.goalPr;
            set => this.SetProperty(ref this.goalPr, value);
        }

        public string LeftPr
        {
            get => this.leftPr;
            set => this.SetProperty(ref this.leftPr, value);
        }

        public string TotalCarb { get => this.totalCarb; set => this.SetProperty(ref this.totalCarb, value); }

        public string GoalCarb { get => this.goalCarb; set => this.SetProperty(ref this.goalCarb, value); }

        public string LeftCarb { get => this.leftCarb; set => this.SetProperty(ref this.leftCarb, value); }

        public string TotalFib { get => this.totalFib; set => this.SetProperty(ref this.totalFib, value); }

        public string GoalFib { get => this.goalFib; set => this.SetProperty(ref this.goalFib, value); }

        public string LeftFib { get => this.leftFib; set => this.SetProperty(ref this.leftFib, value); }

        public string TotalFat { get => this.totalFat; set => this.SetProperty(ref this.totalFat, value); }

        public string GoalFat { get => this.goalFat; set => this.SetProperty(ref this.goalFat, value); }

        public string LeftFat { get => this.leftFat; set => this.SetProperty(ref this.leftFat, value); }

        public string TotalSug { get => this.totalSug; set => this.SetProperty(ref this.totalSug, value); }

        public string GoalSug { get => this.goalSug; set => this.SetProperty(ref this.goalSug, value); }

        public string LeftSug { get => this.leftSug; set => this.SetProperty(ref this.leftSug, value); }

        public string WaterPercent { get => this.waterPercent; set => this.SetProperty(ref this.waterPercent, value); }

        public string WaterGoal { get => this.waterGoal; set => this.SetProperty(ref this.waterGoal, value); }

        // Define properties for R1 to R6
        private string[] rValues = new string[36];

        public string R1 { get => this.rValues[0]; set => this.SetProperty(ref this.rValues[0], value); }

        public string R1Cal { get => this.rValues[1]; set => this.SetProperty(ref this.rValues[1], value); }

        public string R1Diet { get => this.rValues[2]; set => this.SetProperty(ref this.rValues[2], value); }

        public string R1Level { get => this.rValues[3]; set => this.SetProperty(ref this.rValues[3], value); }

        public string R1Time { get => this.rValues[4]; set => this.SetProperty(ref this.rValues[4], value); }

        public string R1MealType { get => this.rValues[5]; set => this.SetProperty(ref this.rValues[5], value); }

        public string R2 { get => this.rValues[6]; set => this.SetProperty(ref this.rValues[6], value); }

        public string R2Cal { get => this.rValues[7]; set => this.SetProperty(ref this.rValues[7], value); }

        public string R2Diet { get => this.rValues[8]; set => this.SetProperty(ref this.rValues[8], value); }

        public string R2Level { get => this.rValues[9]; set => this.SetProperty(ref this.rValues[9], value); }

        public string R2Time { get => this.rValues[10]; set => this.SetProperty(ref this.rValues[10], value); }

        public string R2MealType { get => this.rValues[11]; set => this.SetProperty(ref this.rValues[11], value); }

        public string R3 { get => this.rValues[12]; set => this.SetProperty(ref this.rValues[12], value); }

        public string R3Cal { get => this.rValues[13]; set => this.SetProperty(ref this.rValues[13], value); }

        public string R3Diet { get => this.rValues[14]; set => this.SetProperty(ref this.rValues[14], value); }

        public string R3Level { get => this.rValues[15]; set => this.SetProperty(ref this.rValues[15], value); }

        public string R3Time { get => this.rValues[16]; set => this.SetProperty(ref this.rValues[16], value); }

        public string R3MealType { get => this.rValues[17]; set => this.SetProperty(ref this.rValues[17], value); }

        public string R4 { get => this.rValues[18]; set => this.SetProperty(ref this.rValues[18], value); }

        public string R4Cal { get => this.rValues[19]; set => this.SetProperty(ref this.rValues[19], value); }

        public string R4Diet { get => this.rValues[20]; set => this.SetProperty(ref this.rValues[20], value); }

        public string R4Level { get => this.rValues[21]; set => this.SetProperty(ref this.rValues[21], value); }

        public string R4Time { get => this.rValues[22]; set => this.SetProperty(ref this.rValues[22], value); }

        public string R4MealType { get => this.rValues[23]; set => this.SetProperty(ref this.rValues[23], value); }

        public string R5 { get => this.rValues[24]; set => this.SetProperty(ref this.rValues[24], value); }

        public string R5Cal { get => this.rValues[25]; set => this.SetProperty(ref this.rValues[25], value); }

        public string R5Diet { get => this.rValues[26]; set => this.SetProperty(ref this.rValues[26], value); }

        public string R5Level { get => this.rValues[27]; set => this.SetProperty(ref this.rValues[27], value); }

        public string R5Time { get => this.rValues[28]; set => this.SetProperty(ref this.rValues[28], value); }

        public string R5MealType { get => this.rValues[29]; set => this.SetProperty(ref this.rValues[29], value); }


        public string R6 { get => this.rValues[30]; set => this.SetProperty(ref this.rValues[30], value); }

        public string R6Cal { get => this.rValues[31]; set => this.SetProperty(ref this.rValues[31], value); }

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
        private static int _userId;
        public static int UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public ICommand RefreshMealsCommand { get; }

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
            this.Food_Cal = this.calorieService.GetFood(number_userId).ToString();

            this.BaseGoal_Cal = "2000";
            this.Exercise_Cal = "500";

            float circleTextNr = float.Parse(this.BaseGoal_Cal) - float.Parse(this.Food_Cal) + float.Parse(this.Exercise_Cal);
            this.CircleText = circleTextNr.ToString();

            System.Diagnostics.Debug.WriteLine("Getting macros...");
            // Initialize macros values from database
            this.TotalPr = this.macrosService.GetProteinIntake(number_userId).ToString();
            this.TotalCarb = this.macrosService.GetCarbohydratesIntake(number_userId).ToString();
            this.TotalFat = this.macrosService.GetFatIntake(number_userId).ToString();
            this.TotalFib = this.macrosService.GetFiberIntake(number_userId).ToString();
            this.TotalSug = this.macrosService.GetSugarIntake(number_userId).ToString();

            // Initialize values
            this.GoalPr = "30";
            this.GoalCarb = "200";
            this.GoalFib = "30";
            this.GoalFat = "90";
            this.GoalSug = "50";

            this.LeftPr = (float.Parse(this.GoalPr) - float.Parse(this.TotalPr)).ToString();
            this.LeftCarb = (float.Parse(this.GoalCarb) - float.Parse(this.TotalCarb)).ToString();
            this.LeftFib = (float.Parse(this.GoalFib) - float.Parse(this.TotalFib)).ToString();
            this.LeftFat = (float.Parse(this.GoalFat) - float.Parse(this.TotalFat)).ToString();
            this.LeftSug = (float.Parse(this.GoalSug) - float.Parse(this.TotalSug)).ToString();

            this.WaterGoal = "2000";

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
                this.R1Cal = this.R2Cal = this.R3Cal = this.R4Cal = this.R5Cal = this.R6Cal = defaultCal;
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
                            case 0: this.R1 = recipeName; this.R1Cal = calories; this.R1Diet = diet; this.R1Level = level; this.R1Time = time; this.R1MealType = mealType; break;
                            case 1: this.R2 = recipeName; this.R2Cal = calories; this.R2Diet = diet; this.R2Level = level; this.R2Time = time; this.R2MealType = mealType; break;
                            case 2: this.R3 = recipeName; this.R3Cal = calories; this.R3Diet = diet; this.R3Level = level; this.R3Time = time; this.R3MealType = mealType; break;
                            case 3: this.R4 = recipeName; this.R4Cal = calories; this.R4Diet = diet; this.R4Level = level; this.R4Time = time; this.R4MealType = mealType; break;
                            case 4: this.R5 = recipeName; this.R5Cal = calories; this.R5Diet = diet; this.R5Level = level; this.R5Time = time; this.R5MealType = mealType; break;
                            case 5: this.R6 = recipeName; this.R6Cal = calories; this.R6Diet = diet; this.R6Level = level; this.R6Time = time; this.R6MealType = mealType; break;
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
