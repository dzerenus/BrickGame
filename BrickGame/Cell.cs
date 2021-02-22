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

        /// <summary>
        /// Процедура закраски клетки определённым цветом и контуром.
        /// </summary>
        /// <param name="graph">Поле для рисования.</param>
        /// <param name="brush">Кисть, цветом которой зальют клетку.</param>
        /// <param name="pen">Ручка для рисования контура.</param>
        public void Draw(Graphics graph, Brush brush, Pen pen)
        {
            IsFill = true; // Клетка теперь залита цветом.

            // Сама клетка - прямоугольник.
            var cell = new Rectangle(start.x, start.y, 20, 20);

            // Отрисовка и залитие.
            graph.FillRectangle(brush, cell);
            graph.DrawRectangle(pen, cell);
        }

        /// <summary>
        /// Процедура залития клетки определённым цветом. В основном, используется для стирания отрисованной клетки.
        /// </summary>
        /// <param name="graph">Поле для рисования.</param>
        /// <param name="clr">Цвет, которым нужно залить клетку.</param>
        public void Fill(Graphics graph, Brush clr)
        {
            IsFill = false; // Клетка больше не залита цветом.

            // Сама клетка - прямоугольник.
            var cell = new Rectangle(start.x, start.y, 21, 21);

            // Отрисовка и залитие.
            graph.FillRectangle(clr, cell);
        }
    }
}
