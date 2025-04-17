namespace MealPlannerProject.Pages
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using MealPlannerProject.Services;
    using MealPlannerProject.ViewModels;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;

    public sealed partial class ActivityLevelPage : Page
    {
        [Obsolete]
        private readonly ActivityLevelViewModel viewModel = new ();

        [Obsolete]
        public ActivityLevelPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("ActivityLevelPage initialized successfully.");
                this.DataContext = this.viewModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in ActivityLevelPage constructor: {ex.Message}");
                throw; // Re-throw the exception to be caught by the global handler
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is GoalPageViewModel goalPageViewModel)
            {
                Debug.WriteLine($"GoalPage received user: {goalPageViewModel.FirstName} {goalPageViewModel.LastName}");
                this.viewModel.SetUserInfo(goalPageViewModel.FirstName, goalPageViewModel.LastName);
            }
        }

        private async Task ShowErrorDialog(string v)
        {
            var dialog = new ContentDialog
            {
                Title = "Error",
                Content = v,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot,
            };
            await dialog.ShowAsync();
        }

        [Obsolete]
        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ActivityPageService.ValidateSelectedActivity(this.viewModel.SelectedActivity))
                {
                    await this.ShowErrorDialog("Please select an activity level.");
                    return;
                }

                this.viewModel.GoNext();
            }
            catch (Exception ex)
            {
                await this.ShowErrorDialog($"An error occurred: {ex.Message}");
            }
        }
    }
}
