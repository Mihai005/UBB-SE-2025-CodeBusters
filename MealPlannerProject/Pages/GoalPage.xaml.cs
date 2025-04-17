namespace MealPlannerProject.Pages
{
    using System;
    using System.Diagnostics;
    using MealPlannerProject.ViewModels;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;

    public sealed partial class GoalPage : Page
    {
        private GoalPageViewModel goalPageViewModel;

        public GoalPage()
        {
            try
            {
                this.InitializeComponent();
                this.goalPageViewModel = new GoalPageViewModel();
                this.DataContext = this.goalPageViewModel;
                Debug.WriteLine("GoalPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in GoalPage constructor: {ex.Message}");
                throw;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is BodyMetricsViewModel metricsViewModel)
            {
                Debug.WriteLine($"GoalPage received user: {metricsViewModel.FirstName} {metricsViewModel.LastName}");
                this.goalPageViewModel.SetUserInfo(metricsViewModel.FirstName, metricsViewModel.LastName);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.goalPageViewModel.SelectedGoal))
                {
                    this.ShowErrorDialog("Please select a goal.");
                    return;
                }

                this.goalPageViewModel.GoNext();
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog($"An error occurred: {ex.Message}");
            }
        }

        private async void ShowErrorDialog(string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Error",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot,
            };
            await dialog.ShowAsync();
        }
    }
}
