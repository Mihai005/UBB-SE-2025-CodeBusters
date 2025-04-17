using System;
using Microsoft.UI.Xaml.Data;

namespace MealPlannerProject.Converters
{
    public class GoalOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || parameter == null)
                return 1.0;

            return value.Equals(parameter) ? 0.5 : 1.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException("ConvertBack is not supported.");
        }
    }
}
