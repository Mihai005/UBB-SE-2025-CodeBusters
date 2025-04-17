namespace MealPlannerProject.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.Pages;
    using MealPlannerProject.Services;

    public partial class CookingLevelViewModel : ViewModelBase
    {
        private readonly ICookingPageService cookingPageService;

        private string userFirstName;
        private string userLastName;
        private string userSelectedCookingSkill;

        public ObservableCollection<string> CookingLevels { get; } = new ObservableCollection<string>
        {
            "I am a beginner",
            "I cook sometimes",
            "I love cooking",
            "I prefer quick meals",
            "I meal prep",
        };

        public ICommand NavigateToPreviousPageCommand { get; }

        public ICommand NavigateToNextPageCommand { get; }

        public ICommand NextCommand { get; }

        public CookingLevelViewModel()
        {
            this.NavigateToPreviousPageCommand = new RelayCommand(this.NavigateToPreviousPage);
            this.NavigateToNextPageCommand = new RelayCommand(this.NavigateToNextPage);
            this.NextCommand = new RelayCommand(NavigateToNextPage);
            this.cookingPageService = new CookingPageService();

            this.userFirstName = string.Empty;
            this.userLastName = string.Empty;
            this.userSelectedCookingSkill = string.Empty;

            this.PropertyChanged = (eventSender, eventArguments) => { };
        }

        public string FirstName
        {
            get => this.userFirstName;
            set
            {
                this.userFirstName = value;
                this.OnPropertyChanged(nameof(this.FirstName));
            }
        }

        public string LastName
        {
            get => this.userLastName;
            set
            {
                this.userLastName = value;
                this.OnPropertyChanged(nameof(this.LastName));
            }
        }

        public string SelectedCookingSkill
        {
            get => this.userSelectedCookingSkill;
            set
            {
                this.userSelectedCookingSkill = value;
                this.OnPropertyChanged(nameof(this.SelectedCookingSkill));
            }
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public void NavigateToNextPage()
        {
            this.cookingPageService.AddCookingSkill(
                this.FirstName,
                this.LastName,
                this.SelectedCookingSkill);

            NavigationService.Instance.NavigateTo(typeof(DietaryPreferencesPage), this);
        }

        private void NavigateToPreviousPage()
        {
            NavigationService.Instance.GoBack();
        }

        // Override base class event to provide our own implementation
        public new event PropertyChangedEventHandler PropertyChanged;

        // Override base class method to use our own PropertyChanged event
        protected new void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
