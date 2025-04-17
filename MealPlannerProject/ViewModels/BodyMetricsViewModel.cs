using MealPlannerProject.Services;
using System.Windows.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using MealPlannerProject.Pages;
using MealPlannerProject.Interfaces.Services;
using MealPlannerProject.Interfaces;
using MealPlannerProject.Queries;

namespace MealPlannerProject.ViewModels
{
    public class BodyMetricsViewModel : ViewModelBase
    {
        private readonly IBodyMetricService _bodyMetricService;

        public ICommand SubmitBodyMetricsCommand { get; }

        private string weight = string.Empty;
        private string height = string.Empty;
        private string targetWeight = string.Empty;
        private string firstName = string.Empty;
        private string lastName = string.Empty;

        public BodyMetricsViewModel()
        {
            // Initialize _bodyMetricService with the IDataLink dependency
            _bodyMetricService = new BodyMetricService(DataLink.Instance);
            SubmitBodyMetricsCommand = new RelayCommand(GoNext);
        }

        public string Weight
        {
            get => weight;
            set
            {
                weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        public string Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        public string TargetWeight
        {
            get => targetWeight;
            set
            {
                targetWeight = value;
                OnPropertyChanged(nameof(TargetWeight));
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public BodyMetricsViewModel(IBodyMetricService bodyMetricService)
        {
            _bodyMetricService = bodyMetricService ?? throw new ArgumentNullException(nameof(bodyMetricService));
            SubmitBodyMetricsCommand = new RelayCommand(GoNext);
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private void GoNext()
        {
            try
            {
                this._bodyMetricService.UpdateUserBodyMetrics(this.FirstName, this.LastName, this.Weight, this.Height, this.TargetWeight);
                NavigationService.Instance.NavigateTo(typeof(GoalPage), this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GoNext: {ex.Message}");
            }
        }
    }
}
