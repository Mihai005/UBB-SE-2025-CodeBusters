namespace MealPlannerProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MealPlannerProject.Interfaces;
    using MealPlannerProject.Interfaces.Repositories;
    using MealPlannerProject.Interfaces.Services;
    using MealPlannerProject.Queries;
    using MealPlannerProject.Repositories;
    using Windows.ApplicationModel.Appointments.AppointmentsProvider;

    public class CookingPageService : ICookingPageService
    {
        private readonly ICookingPageRepository CookingPageRepo;

        public CookingPageService()
        {
            this.CookingPageRepo = new CookingPageRepository();
        }

        [Obsolete]
        public void AddCookingSkill(string firstName, string lastName, string cookingDescription)
        {
            Debug.WriteLine($"Adding cooking skill {cookingDescription} for user {firstName} {lastName}");

            int userId = this.CookingPageRepo.GetUserIdByName(firstName, lastName);
            int skillId = this.CookingPageRepo.GetCookingSkillIdByDescription(cookingDescription);

            this.CookingPageRepo.UpdateUserCookingSkill(userId, skillId);
        }
    }
}
