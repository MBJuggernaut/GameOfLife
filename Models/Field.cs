using System;
using System.Linq;

namespace GameOfLife.Models
{
    public class Field
    {
        public bool[,] State { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public byte MinAliveNeighbours;
        public byte MaxAliveNeighbours;

        private bool _hasAliveDots;
        public bool HasAliveDots => _hasAliveDots;

        public Field(int width, int height)
        {
            Width = width;
            Height = height;

            MinAliveNeighbours = 2;
            MaxAliveNeighbours = 3;

            State = new bool[Width, Height];
        }

        public Field(int width, int height, byte minAliveNeighbours, byte maxAliveNeighbours)
        {
            Width = width;
            Height = height;

            MinAliveNeighbours = minAliveNeighbours;
            MaxAliveNeighbours = maxAliveNeighbours;

            State = new bool[Width, Height];
        }

        public bool TrySetState(int x, int y, bool isAlive)
        {
            try
            {
                State[x, y] = isAlive;
                if (isAlive)
                {
                    _hasAliveDots = true;
                }
                return true;
            }
            catch (Exception e)
            {
                //log
                return false;
            }
        }

        public void UpdateState()
        {
            bool[,] newState = (bool[,])State.Clone();
            _hasAliveDots = false;
            for (int w = 0; w < Width; w++)
            {
                for (int h = 0; h < Height; h++)
                {
                    var aliveNeighbourDotsCount = GetDotNeighboursStates(w, h, newState).Where(isAlive => isAlive).Count();
                    bool isAlive = aliveNeighbourDotsCount >= MinAliveNeighbours && aliveNeighbourDotsCount <= MaxAliveNeighbours;
                    if (isAlive)
                    {
                        _hasAliveDots = true;
                    }
                    TrySetState(w, h, isAlive);
                }
            }
        }

        private bool[] GetDotNeighboursStates(int x, int y, bool[,] states)
        {
            bool[] neighbourDotsStates = new bool[8]
            {
                GetDotState(x-1, y-1, states),
                GetDotState(x, y-1, states),
                GetDotState(x+1, y-1, states),
                GetDotState(x-1, y, states),
                GetDotState(x+1, y, states),
                GetDotState(x-1, y+1, states),
                GetDotState(x, y+1, states),
                GetDotState(x+1, y+1, states),
            };

            return neighbourDotsStates;
        }

        private bool GetDotState(int x, int y, bool[,] states)
        {
            if (x < 0)
            {
                x += Width;
            }
            else if (x >= Width)
            {
                x -= Width;
            }

            if (y < 0)
            {
                y += Height;
            }
            else if (y >= Height)
            {
                y -= Height;
            }

            return states[x, y];
        }
    }
}
