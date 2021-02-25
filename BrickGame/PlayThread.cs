using System;
using System.Drawing;
using System.Threading;

namespace BrickGame
{
    class PlayThread
    {
        public bool IsActive = false;

        // Приватные переменные для доступа из разных потоков.
        private Field field;
        private Figure figure;

        private Brush[] activeBrushes;
        private Brush[] deactiveBrushes;


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
        public PlayThread(Graphics g, Brush bBrush, Brush[] active, Brush[] deactive)
        {
            activeBrushes = active;
            deactiveBrushes = deactive;

            field = new Field(14, 28, bBrush, g);
            graph = g;
        }

        /// <summary>
        /// Процедура непосредственного запуска метода игры.
        /// </summary>
        public void ThreadProc()
        {
            field.Update();

            Random rnd = new Random();
            IsActive = true;

            while (IsActive)
            {
                var type = rnd.Next(0, 6);
                var act = activeBrushes[type];
                var deact = deactiveBrushes[type];

                figure = new Figure(type, act, deact);

                while (!figure.IsClosed && IsActive)
                {
                    try { figure.Fall(field); }
                    catch (IndexOutOfRangeException) { 
                        IsActive = false;
                        return;
                    }

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
            if (key == "d" || key == "в") figure.Move(field, 1);   // Переместить вправо.
            if (key == "a" || key == "ф") figure.Move(field, -1);  // Переместить влево.
            if (key == "w" || key == "ц") figure.Turn(field);    // Повернуть.
            if (key == "s" || key == "ы")
            {
                time = 1;
            }          // Быстро опустить.
            if (key == " ") return;                        // Бросить.
        }
    }
}
