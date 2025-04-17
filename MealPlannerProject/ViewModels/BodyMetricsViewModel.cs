namespace MealPlannerProject.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.Pages;
    using MealPlannerProject.Services;

    public class BodyMetricsViewModel : ViewModelBase
    {
        private readonly IBodyMetricService bodyMetricService;

        public ICommand SubmitBodyMetricsCommand { get; }

        private string weight = string.Empty;
        private string height = string.Empty;
        private string targetWeight = string.Empty;
        private string firstName = string.Empty;
        private string lastName = string.Empty;

        public BodyMetricsViewModel()
            : this(new BodyMetricService())
        {
        }

        public string Weight
        {
            get => this.weight;
            set
            {
                this.weight = value;
                this.OnPropertyChanged(nameof(this.Weight));
            }
        }

        public string Height
        {
            get => this.height;
            set
            {
                this.height = value;
                this.OnPropertyChanged(nameof(this.Height));
            }
        }

        public string TargetWeight
        {
            get => this.targetWeight;
            set
            {
                this.targetWeight = value;
                this.OnPropertyChanged(nameof(this.TargetWeight));
            }
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

        public BodyMetricsViewModel(IBodyMetricService bodyMetricService)
        {
            this.bodyMetricService = bodyMetricService ?? throw new ArgumentNullException(nameof(bodyMetricService));
            this.SubmitBodyMetricsCommand = new RelayCommand(this.GoNext);
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public void GoNext()
        {
            try
            {
                this.bodyMetricService.UpdateUserBodyMetrics(this.FirstName, this.LastName, this.Weight, this.Height, this.TargetWeight);
                NavigationService.Instance.NavigateTo(typeof(GoalPage), this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GoNext: {ex.Message}");
            }
        }
    }
}
