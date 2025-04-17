namespace MealPlannerProject.Converters
{
    using System;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Data;

    public class GoalButtonStyleConverter : IValueConverter
    {
        public IResourceProvider? ResourceProvider { get; set; }

        public object? Convert(object value, Type targetType, object parameter, string language)
        {
            string? goal = value as string;

            var resources = this.ResourceProvider ?? new DefaultResourceProvider();

            return goal switch
            {
                "Lose weight" => resources.Get("LoseWeightButtonStyle"),
                "Gain weight" => resources.Get("GainWeightButtonStyle"),
                "Maintain weight" => resources.Get("MaintainWeightButtonStyle"),
                "Body recomposition" => resources.Get("BodyRecompositionButtonStyle"),
                "Improve overall health" => resources.Get("ImproveHealthButtonStyle"),
                _ => null
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public interface IResourceProvider
    {
        object? Get(string key);
    }

    public class DefaultResourceProvider : IResourceProvider
    {
        public object? Get(string key)
        {
            return Application.Current.Resources[key];
        }
    }
}
