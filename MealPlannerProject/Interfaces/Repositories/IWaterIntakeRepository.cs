using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Interfaces.Repositories
{
    public interface IWaterIntakeRepository
    {
        void AddUserIfNotExists(int userId);

        float GetWaterIntake(int userId);

        void UpdateWaterIntake(int userId, float newIntake);
    }
}
