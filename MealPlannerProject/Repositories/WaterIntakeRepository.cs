namespace MealPlannerProject.Repositories
{
    using System.Data.SqlClient;
    using MealPlannerProject.Interfaces;
    using MealPlannerProject.Interfaces.Repositories;
    using MealPlannerProject.Queries;

    public class WaterIntakeRepository : IWaterIntakeRepository
    {
        private readonly IDataLink dataLink;

        public WaterIntakeRepository()
        {
            this.dataLink = DataLink.Instance;
        }

        [System.Obsolete]
        public void AddUserIfNotExists(int userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            var result = this.dataLink.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM water_trackers WHERE u_id = @UserId", parameters, false);

            if (result == 0)
            {
                var insertParams = new SqlParameter[] { new SqlParameter("@UserId", userId) };
                this.dataLink.ExecuteQuery(
                    "INSERT INTO water_trackers (u_id, water_intake, water_goal) VALUES (@UserId, 0, 2000)",
                    insertParams,
                    false);
            }
        }

        [System.Obsolete]
        public float GetWaterIntake(int userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return this.dataLink.ExecuteScalar<float>(
                "SELECT dbo.get_water_intake(@UserId)", parameters, false);
        }

        [System.Obsolete]
        public void UpdateWaterIntake(int userId, float newIntake)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@NewIntake", newIntake)
            };
            this.dataLink.ExecuteQuery("exec update_water_intake @UserId, @NewIntake", parameters, false);
        }
    }
}
