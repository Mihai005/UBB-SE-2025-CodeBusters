namespace MealPlannerProject.Repositories
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using MealPlannerProject.Interfaces;
    using MealPlannerProject.Interfaces.Repositories;
    using MealPlannerProject.Models;
    using MealPlannerProject.Queries;

    public class IngredientRepository : IIngredientRepository
    {
        private readonly IDataLink dataLink;

        // Constructor with Dependency Injection for IDataLink
        public IngredientRepository()
        {
            this.dataLink = DataLink.Instance;
        }

        public IngredientRepository(IDataLink dataLink)
        {
           this.dataLink = dataLink;
        }

        // Async method to fetch ingredient details by name
        [Obsolete]
        public async Task<Ingredient?> GetIngredientByNameAsync(string name)
        {
            const string query = @"SELECT i_id, i_name, calories, protein, carbohydrates, fat, fiber, sugar 
                                   FROM ingredients 
                                   WHERE i_name = @name;";

            var parameters = new SqlParameter[] { new ("@name", name) };

            // Execute SQL query asynchronously
            DataTable result = await Task.Run(() => this.dataLink.ExecuteSqlQuery(query, parameters));

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Ingredient(
                    Convert.ToInt32(row["i_id"]),
                    row["i_name"].ToString(),
                    row["calories"] == DBNull.Value ? 0 : Convert.ToSingle(row["calories"]),
                    row["protein"] == DBNull.Value ? 0 : Convert.ToSingle(row["protein"]),
                    row["carbohydrates"] == DBNull.Value ? 0 : Convert.ToSingle(row["carbohydrates"]),
                    row["fat"] == DBNull.Value ? 0 : Convert.ToSingle(row["fat"]),
                    row["fiber"] == DBNull.Value ? 0 : Convert.ToSingle(row["fiber"]),
                    row["sugar"] == DBNull.Value ? 0 : Convert.ToSingle(row["sugar"]));
            }

            return Ingredient.NoIngredient;
        }
    }
}
