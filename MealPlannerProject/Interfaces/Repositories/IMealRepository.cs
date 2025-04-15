namespace MealPlannerProject.Repositories
{
    using MealPlannerProject.Models;
    using System.Threading.Tasks;

    public interface IMealRepository
    {
        Task<int> CreateMealAsync(Meal meal, int cookingSkillId, int mealTypeId);
        Task<int> AddMealIngredientAsync(int mealId, int ingredientId, float quantity);
    }
}