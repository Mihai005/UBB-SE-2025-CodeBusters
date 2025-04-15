namespace MealPlannerProject.Pages
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using MealPlannerProject.Pages;
    using MealPlannerProject.Services;
    using MealPlannerProject.ViewModels;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Controls.Primitives;
    using Microsoft.UI.Xaml.Data;
    using Microsoft.UI.Xaml.Input;
    using Microsoft.UI.Xaml.Media;
    using Microsoft.UI.Xaml.Navigation;
    using Windows.Foundation;
    using Windows.Foundation.Collections;

    public sealed partial class UserPage : Page
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        private Services.UserPageService userPageService = new Services.UserPageService();

        public UserPage()
        {
            this.InitializeComponent();
        }

        public string GetUserFullName()
        {
            return $"{this.LastName} {this.FirstName}";
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            this.FirstName = this.FirstNameTextBox.Text.Trim();
            this.LastName = this.LastNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(this.FirstName) || string.IsNullOrEmpty(this.LastName))
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Please enter both your first and last name.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot,
                };
                _ = dialog.ShowAsync();
                return;
            }
            int userId = this.userPageService.userHasAnAccount(this.LastName + " " + this.FirstName);
            if (userId != -1)
            {
                GroceryViewModel.UserId = userId;
                AddFoodPageViewModel.UserId = userId;
                MainViewModel.UserId = userId;
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                userId = userPageService.insertNewUser(this.LastName + " " + this.FirstName);
                GroceryViewModel.UserId = userId;
                AddFoodPageViewModel.UserId = userId;
                MainViewModel.UserId = userId;
                this.Frame.Navigate(typeof(BodyMetricsPage), this);
            }
        }
    }
}