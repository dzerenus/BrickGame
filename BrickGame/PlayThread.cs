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
        private Figure figure;

        private Graphics graph;

        private int time = 300;

        // Ручка для отрисовки контура предметов и кисть для отрисовки фона.
        private Pen borderPen = new Pen(Brushes.Black);

        /// <summary>
        /// Инициализация класса нового потока и передача необходимых параметров в него.
        /// Поток используется для отрисовки фигур и процесса игры, чтобы не заставлять интерфейс подвисать.
        /// </summary>
        /// <param name="f">Класс игрового поля.</param>
        /// <param name="g">Поле для рисования фигур.</param>
        public PlayThread(Graphics g, Brush bBrush)
        {
            field = new Field(14, 28, bBrush, g);
            graph = g;
        }

        /// <summary>
        /// Процедура непосредственного запуска метода игры.
        /// </summary>
        public void ThreadProc()
        {
            Random rnd = new Random();
            IsActive = true;

            while (true)
            {
                var type = rnd.Next(0, 6);
                figure = new Figure(type, Brushes.Green, Brushes.Gray);

                while (!figure.IsClosed)
                {
                    FallFigure();

                    Thread.Sleep(time);
                }
                time = 300;

                var lines = field.LinesAnalize();
                if (lines.Count > 0)
                {
                    field.RemoveLines(lines);
                }
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
            if (key == "w" || key == "ц") figure.Turn(field);    // Повернуть.
            if (key == "s" || key == "ы")
            {
                time = 1;
            }          // Быстро опустить.
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

            figure.Erase(field);

            // Смещение каждой клетки фигуры по X.
            for (int i = 0; i < figure.APosition.Length; i++)
            {
                // Ссылочные переменные клетки.
                ref int rY = ref figure.APosition[i].y;
                ref int rX = ref figure.APosition[i].x;

                rX += dX; // Смещаем клетку.
            }

            figure.Draw(field, figure.ActiveColor);
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
                    Thread.Sleep(50);
                    figure.IsClosed = true;
                    break;
                }
            }

            figure.Erase(field);

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
                if (figure.IsClosed)
                {
                    field.Cells[rX, rY].IsClosed = true;
                }
                else rY++; // Опускаем фигуру на одну клетку.
            }

            figure.Draw(field, figure.ActiveColor);
        }
    }
}
