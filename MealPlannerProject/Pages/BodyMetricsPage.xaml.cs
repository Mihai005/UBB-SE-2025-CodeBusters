namespace MealPlannerProject.Pages
{
    using System;
    using System.Diagnostics;
    using MealPlannerProject.ViewModels;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;

    public sealed partial class BodyMetricsPage : Page
    {
        private readonly BodyMetricsViewModel viewModel;

        public BodyMetricsPage()
        {
            try
            {
                this.InitializeComponent();
                this.viewModel = new BodyMetricsViewModel();
                this.DataContext = this.viewModel;
                Debug.WriteLine("BodyMetricsPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in BodyMetricsPage constructor: {ex.Message}");
                throw;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is UserPage userPage)
            {
                this.viewModel.SetUserInfo(userPage.FirstName, userPage.LastName);
                Debug.WriteLine($"Received user name: {userPage.FirstName} {userPage.LastName}");
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.WeightTextBox.Text) || string.IsNullOrWhiteSpace(this.HeightTextBox.Text))
                {
                    this.ShowErrorDialog("Please enter both your weight and height.");
                    return;
                }

                if (!float.TryParse(this.WeightTextBox.Text, out float weight) || !float.TryParse(this.HeightTextBox.Text, out float height))
                {
                    this.ShowErrorDialog("Please enter valid numbers for weight and height.");
                    return;
                }

                if (weight <= 0 || height <= 0)
                {
                    this.ShowErrorDialog("Weight and height must be greater than 0.");
                    return;
                }

                this.viewModel.Weight = this.WeightTextBox.Text;
                this.viewModel.Height = this.HeightTextBox.Text;
                this.viewModel.TargetWeight = this.TargetGoalTextBox.Text;
                this.viewModel.GoNext();
                Debug.WriteLine($"Navigating to GoalPage with {this.viewModel.LastName} and {this.viewModel.FirstName}");
            }
            catch (Exception ex)
            {
                this.ShowErrorDialog($"An error occurred: {ex.Message}");
            }
        }

        private async void ShowErrorDialog(string message)
        {
            var dialog = new ContentDialog
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
