namespace MealPlannerProject.Converters
{
    using System;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Data;

    public class GoalButtonStyleConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, string language)
        {
            string? goal = value as string;
            switch (goal)
            {
                case "Lose weight":
                    return Application.Current.Resources["LoseWeightButtonStyle"] as Style;
                case "Gain weight":
                    return Application.Current.Resources["GainWeightButtonStyle"] as Style;
                case "Maintain weight":
                    return Application.Current.Resources["MaintainWeightButtonStyle"] as Style;
                case "Body recomposition":
                    return Application.Current.Resources["BodyRecompositionButtonStyle"] as Style;
                case "Improve overall health":
                    return Application.Current.Resources["ImproveHealthButtonStyle"] as Style;
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
