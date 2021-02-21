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

        // Класс-поток основной игры.
        // Необходим, чтобы сделать интерфейс независимым от остальной игры.
        public PlayThread(Field f, Graphics g)
        {
            field = f;
            graph = g;
        }

        public void ThreadProc()
        {
            var figY = 0;

            while (figY < field.SizeY)
            {
                if (figY > 0)
                {
                    
                    if (field.Cells[3, figY - 1].IsClosed) 
                        break;

                    if (figY + 1 == field.SizeY)
                        field.Cells[3, figY].IsClosed = true;

                    if (figY + 1 < field.SizeY )
                        if (field.Cells[3, figY + 1].IsClosed) 
                            field.Cells[3, figY].IsClosed = true;

                    ClearCell(3, figY - 1);
                }

                FillCell(3, figY);
                figY++;

                Thread.Sleep(50);
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
