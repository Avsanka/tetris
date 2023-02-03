using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace terisss
{
    public class PlayGrid
    {
        private readonly int[,] grid;
        public int Rows { get; }
        public int Columns { get; }

        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public PlayGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }

        public bool IsInside (int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }
        //проверяет, находится ли точка внутри поля

        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }
        //проверяет, пуста ли ячейка, вернёт 1 если в ячейке 0 и она внутри поля

        public bool IsRowFull(int r)
        {
            for (int i = 0; i < Columns; i++)
            {
                if (grid[r, i] == 0)
                    return false;
            }
            return true;
        }
        //проверяет, заполнен ли ряд

        public bool IsRowEmpty(int r)
        {
            for(int i = 0; i < Columns; i++)
            {
                if (grid[r, i] != 0)
                    return false;
            }
            return true;
        }
        //проверяет, пуст ли ряд

        private void RowClear(int r)
        {
            for (int i = 0; i < Columns; i++)
            {
                grid[r, i] = 0;
            }
        }
        //очищает ряд

        private void MoveRowDown(int r, int numRows)
        {
            for (int i = 0; i < Columns; i++)
            {
                grid[r + numRows, i] = grid[r, i];
                grid[r, i] = 0;
            }
        }
        //сдвигает ряд вниз на заданное число

        public int ClearFullRows()
        {
            int cleared = 0;

            for (int r = Rows - 1; r >= 0; r--)
            {
                if (IsRowFull(r))
                {
                    RowClear(r);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(r, cleared);
                }
            }
            return cleared;
        }
    }
}
