namespace MealPlannerProject.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.Pages;
    using MealPlannerProject.Services;

    public class ActivityLevelViewModel : ViewModelBase
    {
        private string selectedActivity;
        private string firstName;
        private string lastName;

        private IActivityPageService activityPageService = new ActivityPageService();

        public ObservableCollection<string> ActivityLevels { get; } = ActivityPageService.GetActivityLevels();

        public ICommand BackCommand { get; }

        public ICommand NextCommand { get; }

        [System.Obsolete]
        public ActivityLevelViewModel()
        {
            this.selectedActivity = string.Empty;
            this.firstName = string.Empty;
            this.lastName = string.Empty;
            this.BackCommand = new RelayCommand(this.GoBack);
            this.NextCommand = new RelayCommand(this.GoNext);
        }

        private void GoBack()
        {
            NavigationService.Instance.GoBack();
        }

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

        public string SelectedActivity
        {
            get => this.selectedActivity;
            set
            {
                this.selectedActivity = value;
                this.OnPropertyChanged(nameof(this.SelectedActivity));
            }
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        [System.Obsolete]
        public void GoNext()
        {
            this.activityPageService.AddActivity(this.FirstName, this.LastName, this.SelectedActivity);
            NavigationService.Instance.NavigateTo(typeof(CookingLevelPage), this);
        }

        public new event PropertyChangedEventHandler? PropertyChanged;

        protected new void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}