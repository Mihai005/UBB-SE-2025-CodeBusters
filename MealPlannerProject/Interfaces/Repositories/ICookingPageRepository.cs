using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Interfaces.Repositories
{
    public interface ICookingPageRepository
    {
        int GetUserIdByName(string firstName, string lastName);

        int GetCookingSkillIdByDescription(string description);

        void UpdateUserCookingSkill(int userId, int cookingSkillId);
    }
}
