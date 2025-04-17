using MealPlannerProject.Interfaces.Services;
using MealPlannerProject.Pages;
using MealPlannerProject.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MealPlannerProject.ViewModels
{
    public class DietaryPreferencesViewModel : INotifyPropertyChanged
    {
        public ICommand BackCommand { get; set; }

        public ICommand NextCommand { get; set; }

        private readonly IDietaryPreferencesService dietaryPreferencesService;

        public ObservableCollection<string> OtherDietOptions { get; set; }

        public ObservableCollection<string> AllergenOptions { get; set; }

        private string otherDiet;

        public string OtherDiet
        {
            get => this.otherDiet;
            set
            {
                if (this.otherDiet != value)
                {
                    this.otherDiet = value;
                    this.OnPropertyChanged(nameof(this.OtherDiet));
                }
            }
        }

        private string allergens;

        public string Allergens
        {
            get => this.allergens;
            set
            {
                if (this.allergens != value)
                {
                    this.allergens = value;
                    this.OnPropertyChanged(nameof(this.Allergens));
                }
            }
        }

        public DietaryPreferencesViewModel()
        {
            this.dietaryPreferencesService = new DietaryPreferencesService();
            this.BackCommand = new RelayCommand(this.BackAction);
            this.NextCommand = new RelayCommand(this.NextAction);

            // Populate options
            this.OtherDietOptions = new ObservableCollection<string>
        {
            "None", "Mediterranean", "Low-Fat", "Diabetic-Friendly", "Kosher", "Halal",
        };

            this.AllergenOptions = new ObservableCollection<string>
        {
            "None", "Peanuts", "Tree Nuts", "Dairy", "Eggs", "Gluten", "Shellfish", "Soy", "Fish", "Sesame",
        };
        }

        private void BackAction()
        {
            NavigationService.Instance.GoBack();
        }

        public void NextAction()
        {
            this.dietaryPreferencesService.AddAllergyAndDietaryPreference(this.FirstName, this.LastName, this.OtherDiet, this.Allergens);
            NavigationService.Instance.NavigateTo(typeof(YoureAllSetPage), this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string firstName;
        private string lastName;

        public string FirstName
        {
            get => this.firstName;
            set
            {
                this.firstName = value;
                this.OnPropertyChanged(nameof(this.FirstName));
            }
        }

        public string LastName
        {
            get => this.lastName;
            set
            {
                this.lastName = value;
                this.OnPropertyChanged(nameof(this.LastName));
            }
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
