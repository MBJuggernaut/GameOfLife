using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Models
{
    public class Game : IEnumerable<Field>, IEnumerator<Field>
    {
        private List<int> _previousStates;

        private Field _startField;
        private Field _currentField;

        public Game(Field field)
        {
            _startField = field;
            Reset();
        }

        public Field Current => _currentField;
        object IEnumerator.Current => _currentField;

        public IEnumerator<Field> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool MoveNext()
        {
            _currentField.UpdateState();

            var currentStateHash = GetHashCode(_currentField.State);
            var repeated = _previousStates.Contains(currentStateHash);
            var alldead = !_currentField.HasAliveDots;

            if (repeated || alldead)
            {
                return false;
            }

            _previousStates.Add(currentStateHash);
            return true;
        }

        private int GetHashCode(bool[,] x)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _currentField.Height; i++)
            {
                for (int k = 0; k < _currentField.Width; k++)
                {
                    sb.Append(x[i, k]);
                }
            }
            return sb.ToString().GetHashCode();
        }

        public void Reset()
        {
            _currentField = _startField;
            _previousStates = new List<int>();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
