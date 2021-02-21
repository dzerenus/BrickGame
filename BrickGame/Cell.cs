using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickGame
{
    class Cell
    {
        // Занята ли клетка чем-либо.
        public bool IsClosed = false;

        // Кортеж координат левого верхнего угла клетки.
        public (int X, int Y) Start { get { return start; } }
        private (int x, int y) start;

        // Кортеж координат правого нижнего угла клетки.
        public (int X, int Y) End { get { return end; } }
        private (int x, int y) end;

        public Cell(int xCount, int yCount)
        {
            // Рассчитываем верхний левый угол клетки.
            var sx = xCount * 20 + xCount;
            var sy = yCount * 20 + yCount;
            start = (sx, sy);

            // Рассчитываем левый нижний угол клетки.
            var ex = 20 + xCount * 20 + xCount;
            var ey = 20 + yCount * 20 + yCount;
            end = (ex, ey);
        }
    }
}
