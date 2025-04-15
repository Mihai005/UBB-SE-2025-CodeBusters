namespace MealPlannerProject.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using MealPlannerProject.Models;
    using MealPlannerProject.Services;
    using Windows.System;

    public class GroceryViewModel : ObservableObject
    {
        private static int userId;

        public static int UserId
        {
            get => userId;
            set => userId = value;
        }

        private readonly GroceryListService service = new ();

        public ObservableCollection<GroceryIngredient> Ingredients { get; private set; } = new ();

        public ObservableCollection<GroceryIngredient> MostFrequentIngredients { get; private set; }

        public ObservableCollection<GroceryIngredient> RecentlyUsedIngredients { get; private set; }

        private ObservableCollection<SectionModel> sections;

        private string newGroceryIngredientName;

        public ObservableCollection<SectionModel> Sections
        {
            get => this.sections;
            set => this.SetProperty(ref this.sections, value);
        }

        public string NewGroceryIngredientName
        {
            get => this.newGroceryIngredientName;
            set => this.SetProperty(ref this.newGroceryIngredientName, value);
        }

        public RelayCommand<GroceryIngredient> AddGroceryIngredientCommand { get; }

        public GroceryViewModel()
        {
            this.AddGroceryIngredientCommand = new RelayCommand<GroceryIngredient>(this.AddGroceryIngredient);
            this.MostFrequentIngredients = new ObservableCollection<GroceryIngredient>
            {
                new GroceryIngredient { Name = "Tomatoes" },
                new GroceryIngredient { Name = "Onions" },
                new GroceryIngredient { Name = "Garlic" },
            };
            this.RecentlyUsedIngredients = new ObservableCollection<GroceryIngredient>
            {
                new GroceryIngredient { Name = "Olive Oil" },
                new GroceryIngredient { Name = "Salt" },
                new GroceryIngredient { Name = "Pepper" },
            };
            this.Sections = new ObservableCollection<SectionModel>
            {
                new SectionModel { Title = "My List" },
            };
            this.sections = new ();
            this.newGroceryIngredientName = "";

            this.LoadUserGroceryList();
        }

        [System.Obsolete]
        public void AddGroceryIngredient(GroceryIngredient? ingredient = null)
        {
            GroceryIngredient resultIngredient = this.service.AddIngredientToUser(userId, ingredient ?? GroceryIngredient.defaultIngredient, this.newGroceryIngredientName, this.sections);
            if (resultIngredient == GroceryIngredient.defaultIngredient)
            {
                return;
            }
            else
            {
                ingredient = resultIngredient;
            }

            ingredient.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(GroceryIngredient.IsChecked))
                {
                    var ing = (GroceryIngredient)s;
                    this.service.UpdateIsChecked(userId, ing.Id, ing.IsChecked);
                }
            };

            this.Sections[0].Items.Add(ingredient);

            this.NewGroceryIngredientName = string.Empty;
        }


        private void LoadUserGroceryList()
        {
            var ingredientsFromDb = this.service.GetIngredientsForUser(userId);

            this.Sections = new ObservableCollection<SectionModel>
            {
                new SectionModel
                {
                    Title = "My List",
                    Items = new ObservableCollection<GroceryIngredient>(ingredientsFromDb),
                },
            };

            this.OnPropertyChanged(nameof(this.Sections));
        }
    }
}
