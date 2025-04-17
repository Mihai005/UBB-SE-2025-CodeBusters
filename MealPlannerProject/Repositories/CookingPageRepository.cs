namespace MealPlannerProject.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MealPlannerProject.Interfaces;
    using MealPlannerProject.Interfaces.Repositories;
    using MealPlannerProject.Queries;

    public class CookingPageRepository : ICookingPageRepository
    {
        private readonly IDataLink dataLink;

        [Obsolete]
        public CookingPageRepository()
        {
            this.dataLink = DataLink.Instance;
        }

        public CookingPageRepository(IDataLink dataLink)
        {
            this.dataLink = dataLink;
        }

        [Obsolete]
        public int GetUserIdByName(string firstName, string lastName)
        {
            string fullName = lastName + " " + firstName;
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_name", fullName),
            };

            return this.dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);
        }

        [Obsolete]
        public int GetCookingSkillIdByDescription(string description)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@cs_description", description),
            };

            return this.dataLink.ExecuteScalar<int>("SELECT dbo.GetCookingSkillByDescription(@cs_description)", parameters, false);
        }

        [Obsolete]
        public void UpdateUserCookingSkill(int userId, int cookingSkillId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", userId),
                new SqlParameter("@cs_id", cookingSkillId),
            };

            this.dataLink.ExecuteNonQuery("UpdateUserCookingSkill", parameters);
        }
    }
}
