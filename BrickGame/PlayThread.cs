using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace BrickGame
{
    class PlayThread
    {
        public bool IsActive = false;

        // Приватные переменные для доступа из разных потоков.
        private Field field;
        private Graphics graph;
        private Figure figure;

        // Ручка для отрисовки контура предметов и кисть для отрисовки фона.
        private Pen borderPen = new Pen(Brushes.Black);
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
            IsActive = true;

            figure = new Figure(0);

            while (!figure.IsClosed)
            {
                FallFigure();

                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Процедура обработки нажатия клавиш.
        /// </summary>
        /// <param name="key">Имя нажатой клавиши.</param>
        public void KeyPressed(string key)
        {
            if (key == "d" || key == "в") MoveFigure(1);   // Переместить вправо.
            if (key == "a" || key == "ф") MoveFigure(-1);  // Переместить влево.
            if (key == "w" || key == "ц") return;          // Повернуть.
            if (key == "s" || key == "ы") return;          // Быстро опустить.
            if (key == " ") return;                        // Бросить.
        }

        /// <summary>
        /// Процедура смещения фигуры по оси X.
        /// </summary>
        /// <param name="dX">Смещение.</param>
        private void MoveFigure(int dX) {

            // Проверяем доступность смещения каждой клетки фигуры.
            for (int i = 0; i < figure.APosition.Length; i++)
            {
                // Получаем координаты клетки фигуры.
                var y = figure.APosition[i].y;
                var x = figure.APosition[i].x;
                var nX = x + dX; // Координата по X с учётом смещения.

                // Проверяем, чтобы X не выходил за левую и правую границы.
                // Проверяем, чтобы X не залезал на уже установленные фигуры.
                if (figure.IsClosed) return;
                if (nX < 0 || nX >= field.SizeX) return;
                if (y >= 0 && field.Cells[nX, y].IsClosed) return;
            }

            // Смещение каждой клетки фигуры по X.
            for (int i = 0; i < figure.APosition.Length; i++)
            {
                // Ссылочные переменные клетки.
                ref int rY = ref figure.APosition[i].y;
                ref int rX = ref figure.APosition[i].x;

                // Смещаем клетку.
                rX += dX;

                // Код ниже отрисовывет фигуру с защитой от перехвата другим процессом.
                var isOK = false;

                while (!isOK)
                {
                    try
                    {
                        if (rY >= 0)
                        {
                            // Отрисовываем фигуру.
                            field.Cells[rX - dX, rY].Fill(graph, backColor);
                            field.Cells[rX, rY].Draw(graph, Brushes.Orange, borderPen);
                        }

                        isOK = true;
                    }

                    catch (InvalidOperationException) { Thread.Sleep(10); }
                }
            }
        }

        private void FallFigure()
        {
            // Цикл проверки доступности ячейки ниже.
            // В этом цикле мы только проверяем, ничего не изменяя.
            // Проверяем доступность каждой отдельной клеточки.
            for (int i = 0; i < figure.APosition.Length; i++)
            {
                // Координаты каждой клетки фигуры.
                var x = figure.APosition[i].x;
                var y = figure.APosition[i].y;

                // Если клетка фигуры за границой экрана, уходим в следующую итерацию.
                if (y < 0) continue;

                // Если ниже фигуры граница игрового поля или уже уставновленная фигура.
                // Блокируем фигуру и выходим из цикла.
                if (y + 1 >= field.SizeY || field.Cells[x, y + 1].IsClosed)
                {
                    Thread.Sleep(300);
                    figure.IsClosed = true;
                    break;
                }
            }

            // Цикл опускания фигуры на одну клетку вниз.
            // Опускаем каждую отдельную клеточку.
            for (int i = 0; i < figure.APosition.Length; i++)
            {
                // Ссылочные переменные на координаты клетки фигуры.
                ref int rX = ref figure.APosition[i].x;
                ref int rY = ref figure.APosition[i].y;

                // Если клетка за экраном, опускаем её на одну клетку вниз.
                if (rY < -1)
                {
                    rY++;
                    continue;
                }

                // Если фигура установлена, блокируем каждую клетку.
                if (figure.IsClosed) field.Cells[rX, rY].IsClosed = true;

                else
                {
                    // Опускаем фигуру на одну клетку.
                    rY++;

                    // Код ниже отрисовывает фигуру с защитой от перехвата другим процессом.
                    var isOK = false;

                    while (!isOK)
                    {
                        try
                        {
                            // Отрисовываем фигуру.
                            if (rY > 0) field.Cells[rX, rY - 1].Fill(graph, backColor);
                            field.Cells[rX, rY].Draw(graph, Brushes.Orange, borderPen);

                            isOK = true;
                        }

                        catch (InvalidOperationException) { Thread.Sleep(10); }
                    }
                }
            }
        }
    }
}
