using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace BrickGame
{
    class PlayThread
    {
        private Field field;
        private Graphics graph;

        // Ручка для отрисовки контура предметов.
        private Pen borderPen = new Pen(Brushes.Black);

        // Кисть для отрисовки фона игрового поля.
        private Brush backColor = new SolidBrush(Color.FromArgb(255, 230, 230, 230));

        /// <summary>
        /// Инициализация класса нового потока и передача необходимых параметров в него.
        /// Поток используется для отрисовки фигур и процесса игры, чтобы не заставлять интерфейс подвисать.
        /// </summary>
        /// <param name="f">Класс игрового поля.</param>
        /// <param name="g">Поле для рисования фигур.</param>
        public PlayThread(Field f, Graphics g)
        {
            field = f;
            graph = g;
        }

        /// <summary>
        /// Процедура непосредственного запуска метода игры.
        /// </summary>
        public void ThreadProc()
        {
            Figure fg = new Figure(1);

            while (true)
            {

                for (int i = 0; i < fg.APosition.Length; i++)
                {
                    var fgY = fg.APosition[i].y;
                    var fgX = fg.APosition[i].x;

                    if (fgY > 0)
                    {
                        if (field.Cells[fgX, fgY - 1].IsClosed) return;


                        if (fgY >= field.SizeY)
                        {
                            for (int j = 0; j < fg.APosition.Length; j++)
                                field.Cells[fg.APosition[j].x, fg.APosition[j].y - 1].IsClosed = true;
                            return;
                        }


                        if (field.Cells[fg.APosition[i].x, fgY].IsClosed)
                        {
                            for (int j = 0; j < fg.APosition.Length; j++)
                                field.Cells[fg.APosition[j].x, fg.APosition[j].y - 1].IsClosed = true;
                            return;
                        }

                        FillCell(fg.APosition[i].x, fgY);
                        ClearCell(fg.APosition[i].x, fgY - 1);
                    }

                    fg.APosition[i].y += 1;

                }

                Thread.Sleep(200);
            }

        }

        // Метод отрисовки и залития какой-то клетки.
        // Входные параметры определяют номер клетки по X и Y.
        private void FillCell(int x, int y)
        {
            // Координаты левого верхнего угла клетки.
            var rx = field.Cells[x, y].Start.X;
            var ry = field.Cells[x, y].Start.Y;

            // Сама клетка - прямоугольник.
            var cell = new Rectangle(rx, ry, 20, 20);

            // Отрисовка и залитие.
            graph.FillRectangle(Brushes.Aquamarine, cell);
            graph.DrawRectangle(borderPen, cell);
        }

        private void ClearCell(int x, int y)
        {
            // Координаты левого верхнего угла клетки.
            var rx = field.Cells[x, y].Start.X;
            var ry = field.Cells[x, y].Start.Y;

            // Сама клетка - прямоугольник.
            var cell = new Rectangle(rx, ry, 21, 21);

            // Отрисовка и залитие.
            graph.FillRectangle(backColor, cell);
        }
    }
}
