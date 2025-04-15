namespace MealPlannerProject.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action eexecute, Func<bool> ccanExecute = null!)
        {
            this.execute = eexecute ?? throw new ArgumentNullException(nameof(eexecute));
            this.canExecute = ccanExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => this.canExecute == null || this.canExecute();

        public void Execute(object? parameter) => this.execute();

        public void RaiseCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
