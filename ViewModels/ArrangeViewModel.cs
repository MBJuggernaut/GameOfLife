using GameOfLife.Commands;
using GameOfLife.Models;
using System.Collections.Generic;
using System.Windows.Input;


namespace GameOfLife.ViewModels
{
    public class ArrangeViewModel : BaseViewModel
    {
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

        private ICommand _selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                return _selectionChangedCommand;
            }
            set
            {
                _selectionChangedCommand = value;
            }
        }
        public ArrangeViewModel(BaseViewModel parentViewModel) : base(parentViewModel)
        {

            var x = 30;
            var y = 30;
            _cellsGrid = new List<List<Cell>>();

            for (int i = 0; i < y; i++)
            {
                var cell_line = new List<Cell>();
                for (int k = 0; k < x; k++)
                {
                    var cell = new Cell { State = false };
                    cell_line.Add(cell);
                }
                _cellsGrid.Add(cell_line);
            }
            SelectionChangedCommand = new HandleSelection(this);
            SelectedViewModel = this;
        }
    }

}
