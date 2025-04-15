namespace MealPlannerProject.Pages
{
    using System;
    using MealPlannerProject.Models;
    using MealPlannerProject.Services;
    using MealPlannerProject.ViewModels;
    using Microsoft.UI;
    using Microsoft.UI.Windowing;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Controls.Primitives;
    using Microsoft.UI.Xaml.Media.Imaging;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using WinRT.Interop;

    public sealed partial class CreateMealPage : Page
    {
        private readonly CreateMealViewModel viewModel;

        public CreateMealPage()
        {
            this.InitializeComponent();
            this.viewModel = new CreateMealViewModel();
            this.DataContext = this.viewModel;
        }

        private async void ChoosePictureButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(picker, hwnd);

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                this.viewModel.SelectedImage = file;
                var bitmapImage = new BitmapImage();
                using (var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    await bitmapImage.SetSourceAsync(stream);
                }

                this.SelectedImage.Source = bitmapImage;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(this.viewModel.MealName) ||
                    string.IsNullOrWhiteSpace(this.viewModel.SelectedMealType) ||
                    string.IsNullOrWhiteSpace(this.viewModel.CookingTime))
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Validation Error",
                        Content = "Please enter all required fields: meal name, meal type, and cooking time.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot,
                    };

                    await dialog.ShowAsync();
                    return;
                }

                // Create meal with all required properties
                var meal = new Meal
                {
                    Name = this.viewModel.MealName,
                    Recipe = this.viewModel.Directions.Count > 0 ? string.Join("\n", viewModel.Directions) : "No directions provided",
                    Category = this.viewModel.SelectedMealType ?? "Other",
                    CookingLevel = this.viewModel.SelectedCookingLevel ?? "Beginner",
                    PreparationTime = int.TryParse(this.viewModel.CookingTime, out int time) ? time : 0,
                    PhotoLink = this.viewModel.SelectedImage?.Path ?? "default.jpg",
                    Calories = this.viewModel.TotalCalories,
                    Protein = this.viewModel.TotalProtein,
                    Carbohydrates = this.viewModel.TotalCarbs,
                    Fat = this.viewModel.TotalFats,
                    Fiber = this.viewModel.TotalFiber,
                    Sugar = this.viewModel.TotalSugar,
                    CreatedAt = DateTime.Now,
                };

                bool success = await viewModel.CreateMealAsync(meal);

                if (success)
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Success",
                        Content = "Meal saved successfully!",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot,
                    };

                    await dialog.ShowAsync();
                    this.Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "Failed to save meal. Please try again.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot,
                    };

                    await dialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving meal: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = $"An error occurred: {ex.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot,
                };

                await dialog.ShowAsync();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
