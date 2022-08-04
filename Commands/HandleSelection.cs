using GameOfLife.Models;
using GameOfLife.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GameOfLife.Commands
{
    public class HandleSelection : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public ArrangeViewModel model;

        public HandleSelection(ArrangeViewModel model)
        {
            this.model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is Cell cell)
            {
                cell.State = cell.State ? false : true;
            }
            var selectedCount = model.CellsGrid.SelectMany(a => a).Where(c=>c.State).Count();

            if (selectedCount == 5)
            {
                var field = Convert(model.CellsGrid);

                var gameModel = new GameViewModel(field, model);
            }
        }

        private Field Convert(List<List<Cell>> list)
        {
            var height = list.Count;
            var width = list.First().Count;

            Field field = new Field(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    field.TrySetState(k, i, list[i][k].State);
                }
            }

            return field;
        }
    }
}
