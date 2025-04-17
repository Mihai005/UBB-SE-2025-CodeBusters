namespace MealPlannerProject.Models
{
    public class YoureAllSetModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public YoureAllSetModel()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
        }
    }
}
