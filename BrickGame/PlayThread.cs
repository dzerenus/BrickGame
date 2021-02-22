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
            Figure fg = new Figure(0);

            while (true)
            {

                for (int i = 0; i < fg.APosition.Length; i++)
                {
                    // Ссылочные переменные на координаты фигуры.
                    ref int fY = ref fg.APosition[i].y;
                    ref int fX = ref fg.APosition[i].x;

                    if (fY >= 0)
                    {
                        if (fY > 0)
                            if (field.Cells[fX, fY - 1].IsClosed) return;


                        if (fY >= field.SizeY)
                        {
                            for (int j = 0; j < fg.APosition.Length; j++)
                                field.Cells[fX, fY - 1].IsClosed = true;
                            return;
                        }


                        if (field.Cells[fX, fY].IsClosed)
                        {
                            for (int j = 0; j < fg.APosition.Length; j++)
                                field.Cells[fX, fY - 1].IsClosed = true;
                            return;
                        }

                        field.Cells[fX, fY].Draw(graph, Brushes.Orange, borderPen);

                        if (fY > 1)
                        {
                            if (!field.Cells[fX, fY - 2].IsFill)
                                field.Cells[fX, fY - 1].Fill(graph, backColor);
                        }
                        else if (fY > 0)
                            field.Cells[fX, fY - 1].Fill(graph, backColor);

                    }

                    fg.APosition[i].y += 1;

                }

                Thread.Sleep(200);
            }
        }

    }
}
