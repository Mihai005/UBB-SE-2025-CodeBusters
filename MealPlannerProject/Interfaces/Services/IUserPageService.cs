namespace MealPlannerProject.Services
{
    public interface IUserPageService
    {
        int UserHasAnAccount(string name);
        int InsertNewUser(string name);
    }
}
