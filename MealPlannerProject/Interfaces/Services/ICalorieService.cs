namespace MealPlannerProject.Services
{
    public interface ICalorieService
    {
        float GetGoal(int userId);
        float GetFood(int userId);
        float GetExercise(int userId);
    }
}
