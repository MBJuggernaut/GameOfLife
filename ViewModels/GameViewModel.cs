using GameOfLife.Models;
using System.Collections.Generic;
using System.Threading;

namespace GameOfLife.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private Field _field;

        private bool _selectedItem;
        public bool SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private List<List<Cell>> _cellsGrid;
        public List<List<Cell>> CellsGrid
        {
            get
            {
                return _cellsGrid;
            }
            set
            {
                _cellsGrid = value;
                OnPropertyChanged("CellsGrid");
            }
        }

        public GameViewModel(Field field, BaseViewModel parentViewModel) : base(parentViewModel)
        {
            SelectedViewModel = this;
            _field = field;
            CellsGrid = GetCells(field);

            Thread thread = new Thread(new ThreadStart(PlayGame));
            thread.Start();

        }

        private void PlayGame()
        {
            Game game = new Game(_field);
            foreach (var field in game)
            {
                CellsGrid = GetCells(field);
                Thread.Sleep(1000);
            }

            SelectedViewModel = new MainViewModel();
        }

        private List<List<Cell>> GetCells(Field f)
        {
            List<List<Cell>> cells = new List<List<Cell>>();

            for (int i = 0; i < f.Height; i++)
            {
                List<Cell> row = new List<Cell>();
                for (int k = 0; k < f.Width; k++)
                {
                    Cell cell = new Cell { State = f.State[k, i] };
                    row.Add(cell);
                }
                cells.Add(row);
            }

            return cells;
        }
    }
}
