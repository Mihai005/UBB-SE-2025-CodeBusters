namespace MealPlannerProject.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ingredients")]
    public class Ingredient
    {
        [Key]
        [Column("ingredient_id")]
        public int Id { get; set; }

        [Column("m_name")]
        public string Name { get; set; }

        [Column("calories")]
        public float Calories { get; set; }

        [Column("protein")]
        public float Protein { get; set; }

        [Column("carbohydrates")]
        public float Carbs { get; set; }

        [Column("fat")]
        public float Fats { get; set; }

        [Column("fiber")]
        public float Fiber { get; set; }

        [Column("sugar")]
        public float Sugar { get; set; }

        public Ingredient(int id, string name, float calories, float protein, float carbs, float fats, float fiber, float sugar)
        {
            this.Id = id;
            this.Name = name;
            this.Calories = calories;
            this.Protein = protein;
            this.Carbs = carbs;
            this.Fats = fats;
            this.Fiber = fiber;
            this.Sugar = sugar;
        }

        public static Ingredient NoIngredient { get; private set; } = new Ingredient(-1, string.Empty, -1, -1, -1, -1, -1, -1);
    }
}