namespace MealPlannerProject.Converters
{
    using System;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Data;

    public class ActivityLevelButtonStyleConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string activityLevel)
            {
                switch (activityLevel)
                {
                    case "Sedentary":
                        return Application.Current.Resources["SedentaryButtonStyle"] as Style;
                    case "Lightly Active":
                        return Application.Current.Resources["LightlyActiveButtonStyle"] as Style;
                    case "Moderately Active":
                        return Application.Current.Resources["ModeratelyActiveButtonStyle"] as Style;
                    case "Very Active":
                        return Application.Current.Resources["VeryActiveButtonStyle"] as Style;
                    case "Super Active":
                        return Application.Current.Resources["SuperActiveButtonStyle"] as Style;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
