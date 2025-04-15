namespace MealPlannerProject.Repositories
{
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using MealPlannerProject.Interfaces;
    using MealPlannerProject.Interfaces.Repositories;
    using MealPlannerProject.Models;
    using MealPlannerProject.Queries;

    public class MealRepository : IMealRepository
    {
        private readonly IDataLink dataLink;

        public MealRepository(IDataLink dataLink)
        {
            this.dataLink = dataLink;
        }

        [System.Obsolete]
        public MealRepository()
        {
            this.dataLink = DataLink.Instance;
        }

        [System.Obsolete]
        public async Task<int> CreateMealAsync(Meal meal, int cookingSkillId, int mealTypeId)
        {
            string query = @"INSERT INTO meals (m_name, recipe, cs_id, mt_id, preparation_time, servings, protein, calories, carbohydrates, fat, fiber, sugar, photo_link) 
                               VALUES (@m_name, @recipe, @cs_id, @mt_id, @preparation_time, @servings, @protein, @calories, @carbohydrates, @fat, @fiber, @sugar, @photo_link); 
                               SELECT SCOPE_IDENTITY();";

            var parameters = new SqlParameter[]
            {
                    new ("@m_name", meal.Name),
                    new ("@recipe", meal.Recipe ?? "No directions provided"),
                    new ("@cs_id", cookingSkillId),
                    new ("@mt_id", mealTypeId),
                    new ("@preparation_time", meal.PreparationTime),
                    new ("@servings", meal.Servings),
                    new ("@protein", meal.Protein),
                    new ("@calories", meal.Calories),
                    new ("@carbohydrates", meal.Carbohydrates),
                    new ("@fat", meal.Fat),
                    new ("@fiber", meal.Fiber),
                    new ("@sugar", meal.Sugar),
                    new ("@photo_link", meal.PhotoLink ?? "default.jpg"),
            };

            return await Task.FromResult(this.dataLink.ExecuteScalar<int>(query, parameters, false));
        }

        [System.Obsolete]
        public async Task<int> AddMealIngredientAsync(int mealId, int ingredientId, float quantity)
        {
            var parameters = new SqlParameter[]
            {
        new ("@mealId", mealId),
        new ("@ingredientId", ingredientId),
        new ("@quantity", quantity),
            };

            int result = this.dataLink.ExecuteNonQuery("InsertMealIngredient", parameters);
            return await Task.FromResult(result);
        }

    }
}
