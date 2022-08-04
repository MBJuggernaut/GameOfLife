using GameOfLife.ViewModels;
using System;
using System.Windows.Input;

namespace GameOfLife.Commands
{
    class StartGameCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public BaseViewModel viewModel;

        public StartGameCommand(BaseViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _ = new ArrangeViewModel(viewModel);
        }
    }
}
