using MealPlannerProject.Queries;
using System.Data;
using System.Data.SqlClient;

namespace MealPlannerProject.Services
{
    public class UserPageService : IUserPageService
    {
        public int UserHasAnAccount(string name)
        {
            var parameters = new SqlParameter[]
            {
                   new SqlParameter("@u_name", name)
            };

            // Specify the type argument explicitly to resolve CS0411  
            int? userId = DataLink.Instance.ExecuteScalar<int?>("SELECT dbo.GetUserByName(@u_name)", parameters, false);
            return userId.HasValue && userId.Value > 0 ? userId.Value : -1;
        }

        public int InsertNewUser(string name)
        {
            var parameters = new SqlParameter[]
            {
                   new SqlParameter("@u_name", name),
                   new SqlParameter("@id", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            DataLink.Instance.ExecuteNonQuery("InsertNewUser", parameters);
            return (int)parameters[1].Value;
        }
    }
}
