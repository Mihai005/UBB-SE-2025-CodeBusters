namespace MealPlannerProject.Pages
{
    using System;
    using System.Diagnostics;
    using MealPlannerProject.ViewModels;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;

    public sealed partial class DietaryPreferencesPage : Page
    {
        private readonly DietaryPreferencesViewModel dietaryPreferencesViewModel;

        public DietaryPreferencesPage()
        {
            this.InitializeComponent();
            this.dietaryPreferencesViewModel = new DietaryPreferencesViewModel();
            this.DataContext = this.dietaryPreferencesViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is CookingLevelViewModel cookingLevelViewModel)
            {
                Debug.WriteLine($"Dietary Preferences page received user: {cookingLevelViewModel.FirstName} {cookingLevelViewModel.LastName}");
                this.dietaryPreferencesViewModel.SetUserInfo(cookingLevelViewModel.FirstName, cookingLevelViewModel.LastName);
            }
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dietaryPreferencesViewModel.NextAction();
            }
            catch (Exception ex)
            {
                var contentDialog = new ContentDialog()
                {
                    Title = "Error",
                    Content = ex.Message,
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot,
                };
                await contentDialog.ShowAsync();
            }
        }

        private void OnBackCommandExecuted()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void OnNextCommandExecuted()
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(YoureAllSetPage), this.dietaryPreferencesViewModel);
            }
        }
    }
}
