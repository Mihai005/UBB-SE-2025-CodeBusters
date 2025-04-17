using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlannerProject.Interfaces.Repositories
{
    public interface IDietaryPreferencesRepository
    {
        [Obsolete]
        void AddAllergyAndDietaryPreference(string firstName, string lastName, string dietaryPreference, string allergy);
    }
}
