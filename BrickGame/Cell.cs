using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BrickGame
{
    class Cell
    {
        public bool IsClosed = false; // Занята ли клетка фигурой.
        public bool IsFill = false;   // Залита ли клетка цветом.

        /// <summary>
        /// Цвет клетки.
        /// </summary>
        public Brush CellColor { get; set; }

        private Pen borderPen;
        private Brush backBrush;
        private Graphics graph;

        // Кортеж координат левого верхнего угла клетки.
        public (int X, int Y) Start { get { return start; } }
        private (int x, int y) start;

        // Кортеж координат правого нижнего угла клетки.
        public (int X, int Y) End { get { return end; } }
        private (int x, int y) end;

        /// <summary>
        /// Класс клетки. Из клеток состоит игровое поле.
        /// </summary>
        /// <param name="xCount">Номер клетки по оси X.</param>
        /// <param name="yCount">Номер клетки по оси Y.</param>
        /// <param name="bPen">Ручка для отрисовки границ клеток.</param>
        public Cell(int xCount, int yCount, Pen bPen, Brush bBrush, Graphics grph)
        {
            graph = grph;
            borderPen = bPen;
            backBrush = bBrush;
            CellColor = backBrush;

            // Рассчитываем верхний левый угол клетки.
            var sx = xCount * 20 + xCount;
            var sy = yCount * 20 + yCount;
            start = (sx, sy);

            // Рассчитываем левый нижний угол клетки.
            var ex = 20 + xCount * 20 + xCount;
            var ey = 20 + yCount * 20 + yCount;
            end = (ex, ey);
        }

        /// <summary>
        /// Процедура закраски клетки определённым цветом и контуром.
        /// </summary>
        /// <param name="brush">Кисть, цветом которой зальют клетку.</param>
        public void Draw(Brush brush)
        {
            CellColor = brush;
            IsFill = true; // Клетка теперь залита цветом.

            // Сама клетка - прямоугольник.
            var cell = new Rectangle(start.x, start.y, 20, 20);

            // Отрисовка и залитие.
            graph.FillRectangle(brush, cell);
            graph.DrawRectangle(borderPen, cell);
            graph.DrawLine(borderPen, start.x + 2, start.y + 2, start.x + 18, start.y + 2);
            graph.DrawLine(borderPen, start.x + 18, start.y + 2, start.x + 18, start.y + 18);
        }

        /// <summary>
        /// Процедура залития клетки определённым цветом. В основном, используется для стирания отрисованной клетки.
        /// </summary>
        public void Fill()
        {
            CellColor = backBrush;

            IsFill = false; // Клетка больше не залита цветом.

            // Сама клетка - прямоугольник.
            var cell = new Rectangle(start.x, start.y, 21, 21);

            // Отрисовка и залитие.
            graph.FillRectangle(backBrush, cell);
        }
    }
}
