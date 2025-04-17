namespace MealPlannerProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class DailyMealModel
    {
        [Key]
        public int DmId { get; set; } // Primary Key

        public int? UserId { get; set; } // Foreign Key to Users, now optional

        [Required]
        public int MealId { get; set; } // Foreign Key to Meals

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateEaten { get; set; } = DateTime.Today; // Default to today

        [Required]
        public float Servings { get; set; } = 1; // Default value

        [Required]
        public string? UnitName { get; set; } // Foreign Key reference to serving_units

        public float? TotalCalories { get; set; }

        public float? TotalProtein { get; set; }

        public float? TotalCarbohydrates { get; set; }

        public float? TotalFat { get; set; }

        public float? TotalFiber { get; set; }

        public float? TotalSugar { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual UserModel? UserModel { get; set; }

        [ForeignKey("MealId")]
        public virtual Meal? Meal { get; set; }
    }
}
