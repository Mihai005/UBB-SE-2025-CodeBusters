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
using MealPlannerProject.ViewModels;
using Microsoft.UI.Xaml.Automation.Peers;

    public sealed partial class DietaryPreferencesPage : Page
    {

        private readonly DietaryPreferencesViewModel dietaryPreferencesViewModel;

        public DietaryPreferencesPage()
        {
            this.InitializeComponent();
            dietaryPreferencesViewModel = new DietaryPreferencesViewModel();
            this.DataContext = dietaryPreferencesViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is CookingLevelViewModel cookingLevelViewModel)
            {
                Debug.WriteLine($"Dietary Preferences page received user: {cookingLevelViewModel.FirstName} {cookingLevelViewModel.LastName}");
                dietaryPreferencesViewModel.SetUserInfo(cookingLevelViewModel.FirstName, cookingLevelViewModel.LastName);
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
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnNextCommandExecuted()
        {
            if (Frame != null)
            {
                Frame.Navigate(typeof(YoureAllSetPage), dietaryPreferencesViewModel);
            }
        }
    }
}
