namespace MealPlannerProject.Services
{
    using System.Data.SqlClient;
    using System.Diagnostics;
    using MealPlannerProject.Exceptions;
    using MealPlannerProject.Interfaces;
    using MealPlannerProject.Queries;

    public class GoalPageService : IGoalPageService
    {
        private readonly IDataLink dataLink;

        [System.Obsolete]
        public GoalPageService(IDataLink? dataLink = null)
        {
            this.dataLink = dataLink ?? DataLink.Instance; // Default to singleton if none provided
        }

        [System.Obsolete]
        public void AddGoals(string firstName, string lastName, string g_description)
        {
            Debug.WriteLine($"Adding goal {g_description} for user {firstName} {lastName}");
            string u_name = lastName + " " + firstName;
            var parameters = new SqlParameter[]
            {
        new SqlParameter("@u_name", u_name),
            };

            Debug.WriteLine($"User name: {u_name}");

            int u_id = this.dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);

            // Check if the user ID is valid
            if (u_id == 0)
            {
                throw new DatabaseOperationException($"User {u_name} not found.");
            }

            Debug.WriteLine($"User ID: {u_id}");

            parameters = new SqlParameter[]
            {
        new SqlParameter("@g_description", g_description),
            };

            int g_id = this.dataLink.ExecuteScalar<int>("SELECT dbo.GetGoalByDescription(@g_description)", parameters, false);

            // Check if the goal ID is valid
            if (g_id == 0)
            {
                throw new DatabaseOperationException($"Goal {g_description} not found.");
            }

            parameters = new SqlParameter[]
            {
        new SqlParameter("@u_id", u_id),
        new SqlParameter("@g_id", g_id),
            };

            this.dataLink.ExecuteNonQuery("UpdateUserGoals", parameters);
        }

    }
}