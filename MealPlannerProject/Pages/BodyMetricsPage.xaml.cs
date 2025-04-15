using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Diagnostics;
using MealPlannerProject.Services;
using MealPlannerProject.ViewModels;
using MealPlannerProject.Queries;

namespace MealPlannerProject.Pages
{
    public sealed partial class BodyMetricsPage : Page
    {
        public BodyMetricsViewModel viewModel;

        public BodyMetricsPage()
        {
            try
            {
                this.InitializeComponent();
                viewModel = new BodyMetricsViewModel(new BodyMetricService(DataLink.Instance));
                this.DataContext = viewModel;
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
                viewModel.SetUserInfo(userPage.FirstName, userPage.LastName);
                Debug.WriteLine($"Received user name: {userPage.FirstName} {userPage.LastName}");
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(WeightTextBox.Text) || string.IsNullOrWhiteSpace(HeightTextBox.Text))
                {
                    ShowErrorDialog("Please enter both your weight and height.");
                    return;
                }

                if (!float.TryParse(WeightTextBox.Text, out float weight) || !float.TryParse(HeightTextBox.Text, out float height))
                {
                    ShowErrorDialog("Please enter valid numbers for weight and height.");
                    return;
                }

                if (weight <= 0 || height <= 0)
                {
                    ShowErrorDialog("Weight and height must be greater than 0.");
                    return;
                }

                viewModel.Weight = WeightTextBox.Text;
                viewModel.Height = HeightTextBox.Text;
                viewModel.TargetWeight = TargetGoalTextBox.Text;
                Debug.WriteLine($"Navigating to GoalPage with {viewModel.LastName} and {viewModel.FirstName}");
            }
            catch (Exception ex)
            {
                ShowErrorDialog($"An error occurred: {ex.Message}");
            }
        }

        private async void ShowErrorDialog(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Error",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await dialog.ShowAsync();
        }
    }
}
