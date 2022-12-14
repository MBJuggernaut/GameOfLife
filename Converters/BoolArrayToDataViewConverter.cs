using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace GameOfLife.Converters
{
    public class BoolArrayToDataViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var array = value as bool[,];
            if (array == null) return null;

            var rows = array.GetLength(0);
            if (rows == 0) return null;

            var columns = array.GetLength(1);
            if (columns == 0) return null;

            var t = new DataTable();

            // Add columns with name "0", "1", "2", ...
            for (var c = 0; c < columns; c++)
            {
                t.Columns.Add(new DataColumn(c.ToString()));
            }

            // Add data to DataTable
            for (var r = 0; r < rows; r++)
            {
                var newRow = t.NewRow();
                for (var c = 0; c < columns; c++)
                {
                    newRow[c] = array[r, c] == true ? 1 : 0;
                }
                t.Rows.Add(newRow);
            }

            return t.DefaultView;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var array = value as DataView;

            return new bool[,]
            {
                {false }
            };
        }
    }

    public static class CustomArray<T>
    {
        public static T[] GetColumn(T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        public static T[] GetRow(T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }
}
