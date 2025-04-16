namespace MealPlannerProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.ViewModels;
    using Microsoft.UI.Xaml.Controls;
    using Windows.Networking.Connectivity;

    public class NavigationService : INavigationService
    {
        private static NavigationService? instance;
        private Frame? mainFrame;

        public static NavigationService Instance => instance ??= new NavigationService();

        public void Initialize(Frame mainFrame)
        {
            this.mainFrame = mainFrame;
        }

        public void NavigateTo(Type pageType, object? parameter = null)
        {
            this.mainFrame?.Navigate(pageType, parameter);
        }

        public void GoBack()
        {
            if (this.mainFrame?.CanGoBack == true)
            {
                this.mainFrame.GoBack();
            }
        }

        internal void NavigateTo(Type type, BodyMetricsViewModel bodyMetricsViewModel)
        {
            this.mainFrame?.Navigate(type, bodyMetricsViewModel);
        }

        internal void NavigateTo(Type type, GoalPageViewModel goalPageViewModel)
        {
            this.mainFrame?.Navigate(type, goalPageViewModel);
        }

        internal void NavigateTo(Type type, ActivityLevelViewModel activityLevelViewModel)
        {
            this.mainFrame?.Navigate(type, activityLevelViewModel);
        }

        internal void NavigateTo(Type type, CookingLevelViewModel cookingLevelViewModel)
        {
            // throw new NotImplementedException();
            this.mainFrame?.Navigate(type, cookingLevelViewModel);
        }
    }
}
