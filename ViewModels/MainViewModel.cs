using GameOfLife.Commands;
using System.Windows.Input;

namespace GameOfLife.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand _startGame;
        public ICommand StartGame
        {
            get
            {
                return _startGame;
            }
            set
            {
                _startGame = value;
            }
        }

        public MainViewModel(): base(null)
        {
            SelectedViewModel = this;
            StartGame = new StartGameCommand(this);
        }
    }
}

