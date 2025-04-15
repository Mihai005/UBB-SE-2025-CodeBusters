using MealPlannerProject.Interfaces;
using MealPlannerProject.Queries;
using System.Data.SqlClient;

namespace MealPlannerProject.Services
{
    public class CalorieService : ICalorieService
    {
        private readonly IDataLink _dataLink;

        public CalorieService()
        {
        }

        public CalorieService(IDataLink dataLink)
        {
            _dataLink = dataLink;
        }

        public float GetGoal(int userId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            return _dataLink.ExecuteScalar<float>("SELECT dbo.get_calorie_goal(@UserId)", parameters, false);
        }

        public float GetFood(int userId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            return _dataLink.ExecuteScalar<float>("SELECT dbo.get_calorie_food(@UserId)", parameters, false);
        }

        public float GetExercise(int userId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            return _dataLink.ExecuteScalar<float>("SELECT dbo.get_calorie_exercise(@UserId)", parameters, false);
        }
    }
}
