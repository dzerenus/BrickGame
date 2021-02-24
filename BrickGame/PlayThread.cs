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

        private int time = 300;

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
            Random rnd = new Random();
            IsActive = true;

            while (true)
            {
                var type = rnd.Next(0, 6);
                figure = new Figure(type);

                while (!figure.IsClosed)
                {
                    FallFigure();

                    Thread.Sleep(time);
                }
                time = 300;

                var lines = FieldAnalyze();
                if (lines.Count > 0)
                {
                    DeleteLines(lines);
                }
            }
        }

        private void DeleteLines(List<int> linesNum)
        {
            for (int i = 0; i < linesNum.Count; i++)
            {
                var y = linesNum[i] + i;

                for (int x = 0; x < field.SizeX; x++)
                {
                    field.Cells[x, y].Fill(graph, backColor);
                    field.Cells[x, y].IsClosed = false;
                }

                for (int u = y - 1; u > 0; u--)
                {
                    for (int x = 0; x < field.SizeX; x++)
                    {
                        if (field.Cells[x, u].IsClosed)
                        {
                            field.Cells[x, u].IsClosed = false;
                            field.Cells[x, u].Fill(graph, backColor);
                            field.Cells[x, u+1].Draw(graph, Brushes.Red, borderPen);
                            field.Cells[x, u+1].IsClosed = true;
                        }
                    }
                }
            }
        }

        private List<int> FieldAnalyze()
        {
            List<int> numLines = new List<int>();

            for (int y = field.SizeY - 1; y >= 0; y--)
            {
                var cellCounter = 0;

                for (int x = field.SizeX - 1; x >= 0; x--)
                {
                    if (field.Cells[x, y].IsClosed) 
                        cellCounter++;
                    else break;
                }

                if (cellCounter == field.SizeX) 
                    numLines.Add(y);
            }

            return numLines;
        }

        /// <summary>
        /// Процедура обработки нажатия клавиш.
        /// </summary>
        /// <param name="key">Имя нажатой клавиши.</param>
        public void KeyPressed(string key)
        {
            if (key == "d" || key == "в") MoveFigure(1);   // Переместить вправо.
            if (key == "a" || key == "ф") MoveFigure(-1);  // Переместить влево.
            if (key == "w" || key == "ц") TurnFigure();    // Повернуть.
            if (key == "s" || key == "ы")
            {
                time = 1;
            }          // Быстро опустить.
            if (key == " ") return;                        // Бросить.
        }

        private void TurnFigure()
        {
            // Если фигура - квадрат, то выходим.
            if (figure.Type == 2) return;

            for (int i = 0; i < figure.APosition.Length; i++)
            {
                var ax = figure.APosition[i].x;
                var ay = figure.APosition[i].y;

                var rx = figure.RPosition[i].x;
                var ry = figure.RPosition[i].y;

                ax = ax - rx;
                ay = ay - ry;

                var temp = rx;
                rx = ry;
                ry = temp;

                if (figure.Type == 3 || figure.Type == 4)
                {
                    if (figure.Stage % 2 == 0) rx *= -1;
                    else ry *= -1;
                }

                else if (figure.Type == 5 || figure.Type == 6) ry *= -1;

                else
                    if (figure.Stage % 2 == 0)
                    {
                        rx *= -1;
                        ry *= -1;
                    }

                ax = ax + rx;
                ay = ay + ry;

                if (ax < 0 || ax >= field.SizeX) return;
                if (ay >= field.SizeY) return;
                if (field.Cells[ax, ay].IsClosed) return;
            }

            figure.Erase(graph, field, backColor);

            for (int i = 0; i < figure.APosition.Length; i++)
            {
                ref int rAX = ref figure.APosition[i].x;
                ref int rAY = ref figure.APosition[i].y;

                ref int rRX = ref figure.RPosition[i].x;
                ref int rRY = ref figure.RPosition[i].y;

                rAX = rAX - rRX;
                rAY = rAY - rRY;

                var temp = rRX;
                rRX = rRY;
                rRY = temp;

                if (figure.Type == 3 || figure.Type == 4)
                {
                    if (figure.Stage % 2 == 0) rRX *= -1;
                    else rRY *= -1;
                }

                else if (figure.Type == 5 || figure.Type == 6) rRY *= -1;

                else
                    if (figure.Stage % 2 == 0)
                    {
                        rRX *= -1;
                        rRY *= -1;
                    }                

                rAX = rAX + rRX;
                rAY = rAY + rRY;
            }

            figure.Draw(graph, field, Brushes.Red, borderPen);

            figure.Stage++;
            if (figure.Stage == 4) figure.Stage = 0;
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

            figure.Erase(graph, field, backColor);

            // Смещение каждой клетки фигуры по X.
            for (int i = 0; i < figure.APosition.Length; i++)
            {
                // Ссылочные переменные клетки.
                ref int rY = ref figure.APosition[i].y;
                ref int rX = ref figure.APosition[i].x;

                rX += dX; // Смещаем клетку.
            }

            figure.Draw(graph, field, Brushes.Red, borderPen);
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

            figure.Erase(graph, field, backColor);

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

            figure.Draw(graph, field, Brushes.Red, borderPen);
        }
    }
}
