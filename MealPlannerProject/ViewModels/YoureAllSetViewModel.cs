namespace MealPlannerProject.ViewModels
{
    using System.ComponentModel;
    using System.Windows.Input;
    using MealPlannerProject.Models;
    using MealPlannerProject.Pages;
    using MealPlannerProject.Services;

    public class YoureAllSetViewModel
    {
        public ICommand NextCommand { get; }

        private readonly YoureAllSetModel model;

        public YoureAllSetViewModel()
        {
            this.NextCommand = new RelayCommand(this.GoNext);
            this.model = new YoureAllSetModel();
        }

        public string FirstName
        {
            get => this.model.FirstName;
            set
            {
                this.model.FirstName = value;
                this.OnPropertyChanged(nameof(this.model.FirstName));
            }
        }

        public string LastName
        {
            get => this.model.LastName;
            set
            {
                this.model.LastName = value;
                this.OnPropertyChanged(nameof(this.model.LastName));
            }
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GoNext()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage), this);
        }
    }
}
