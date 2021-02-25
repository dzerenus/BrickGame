using System;
using System.Drawing;
using System.Threading;

namespace BrickGame
{
    class Figure
    {
        /// <summary>
        /// Заблокирована ли фигура.
        /// </summary>
        public bool IsClosed = false;

        private int type;              // Тип фигуры.
        private bool isMinus = false;  // Способ поворота фигуры.

        /// <summary>
        /// Массив относительных координат клеток фигуры. В массиве 4 элемента, т. к. в тетрисе все фигуры состоят из 4 клеток.
        /// </summary>
        public (int x, int y)[] RPosition { get { return rPosition; } }
        private (int x, int y)[] rPosition = new (int x, int y)[4];

        /// <summary>
        /// Абсполютная позиция фигуры на поле относительно ключевой клетки фигуры, относительные координаты которой равны нулю.
        /// </summary>
        public (int x, int y)[] APosition
        {
            get { return aPosition; }
            set { aPosition = value; }
        }
        private (int x, int y)[] aPosition = new (int x, int y)[4];

        public Brush ActiveColor { get; set; }    // Цвет фигуры в полёте.
        public Brush DeactiveColor { get; set; }  // Цвет фигуры при установке.

        /// <summary>
        /// Класс, использующийся, для создания и отрисовки фигур в момент их падения на поле.
        /// </summary>
        /// <param name="ftype">Тип фигуры. От 0 до 6.</param>
        /// <param name="xSpawnPosition">Координата появления фигуры на поле по X.</param>
        public Figure(int ftype, Brush aColor, Brush dColor, int xSpawnPosition = 6)
        {
            type = ftype;
            ActiveColor = aColor;
            DeactiveColor = dColor;

            switch (type)
            {
                // Если фигура - палка.
                case 0:
                    rPosition[0] = (0, 1);
                    rPosition[1] = (0, 0);
                    rPosition[2] = (0, -1);
                    rPosition[3] = (0, -2);
                    break;

                // Если фигура - трезубец.
                case 1:
                    rPosition[0] = (0, 0);
                    rPosition[1] = (0, -1);
                    rPosition[2] = (-1, 0);
                    rPosition[3] = (1, 0);
                    break;

                // Если фигура - квадрат.
                case 2:
                    rPosition[0] = (0, 0);
                    rPosition[1] = (0, -1);
                    rPosition[2] = (1, -1);
                    rPosition[3] = (1, 0);
                    break;

                // Если фигура - Z.
                case 3:
                    rPosition[0] = (0, 0);
                    rPosition[1] = (0, -1);
                    rPosition[2] = (1, -1);
                    rPosition[3] = (-1, 0);
                    break;

                // Если фигура - обратная Z.
                case 4:
                    rPosition[0] = (0, 0);
                    rPosition[1] = (0, -1);
                    rPosition[2] = (-1, -1);
                    rPosition[3] = (1, 0);
                    break;

                // Если фигура - L.
                case 5:
                    rPosition[0] = (0, -1);
                    rPosition[1] = (0, 0);
                    rPosition[2] = (0, 1);
                    rPosition[3] = (1, 1);
                    break;

                // Если фигура - обратная L.
                case 6:
                    rPosition[0] = (0, -1);
                    rPosition[1] = (0, 0);
                    rPosition[2] = (0, 1);
                    rPosition[3] = (-1, 1);
                    break;

                // Если тип фигуры задан ошибочно.
                default: throw new Exception("Неизвестный тип фигуры"); 
            }

            aPosition[0] = (xSpawnPosition + rPosition[0].x, rPosition[0].y);
            aPosition[1] = (xSpawnPosition + rPosition[1].x, rPosition[1].y);
            aPosition[2] = (xSpawnPosition + rPosition[2].x, rPosition[2].y);
            aPosition[3] = (xSpawnPosition + rPosition[3].x, rPosition[3].y);
        }

        /// <summary>
        /// Процедура стирания фигуры с поля игры.
        /// </summary>
        /// <param name="graph">Поле для рисования.</param>
        /// <param name="field">Поле из клеток.</param>
        /// <param name="BackColor">Фоновой цвет.</param>
        public void Erase(Field field)
        {
            bool isOk = false;

            while (!isOk)
            {
                try
                {
                    foreach (var pos in aPosition)
                    {
                        if (pos.y < 0) continue;
                        field.Cells[pos.x, pos.y].Fill();
                    }

                    isOk = true;
                }

                catch (InvalidOperationException) { Thread.Sleep(15); }
            }
        }

        /// <summary>
        /// Процедура отрисовки фигуры на игровом поле.
        /// </summary>
        /// <param name="field">Поле для рисования.</param>
        public void Draw(Field field, Brush cellBrush)
        {
            bool isOk = false;

            while (!isOk)
            {
                try
                {
                    foreach (var pos in aPosition)
                    { 
                        if (pos.y < 0) continue; 
                        field.Cells[pos.x, pos.y].Draw(cellBrush);
                    }

                    isOk = true;
                }

                catch (InvalidOperationException) { Thread.Sleep(10); }
            }
        }

        /// <summary>
        /// ППроцедура поворота фигуры.
        /// </summary>
        /// <param name="field">Поле отрисовки.</param>
        public void Turn(Field field)
        {
            var check = CanTurn(field);
            if (!check.result) return;

            this.Erase(field); // Стираем фигуру с поля.

            for (int i = 0; i < aPosition.Length; i++)
            {
                aPosition[i] = check.newAPosition[i];
                rPosition[i] = check.newRPosition[i];
            }

            this.Draw(field, ActiveColor); // Рисуем фигуру.
            isMinus = !isMinus;            // Меняем знак минуса.
        }

        private (bool result, (int x, int y)[] newAPosition, (int x, int y)[] newRPosition) CanTurn(Field field)
        {
            // Возвращаемое значение по умолчанию.
            (bool result, (int x, int y)[] nAP, (int x, int y)[] nRP) answer;
            answer.result = false;
            answer.nAP = new (int x, int y)[4];
            answer.nRP = new (int x, int y)[4];

            // Если фигура - квадрат, то выходим.
            if (type == 2) return answer;

            // Цикл проверки фигуры на возможность поворота.
            for (int i = 0; i < aPosition.Length; i++)
            {
                var ax = aPosition[i].x;
                var ay = aPosition[i].y;

                var rx = rPosition[i].x;
                var ry = rPosition[i].y;

                ax = ax - rx;
                ay = ay - ry;

                var temp = rx;
                rx = ry;
                ry = temp;

                if (type == 5 || type == 6) ry *= -1;

                else
                {
                    if (!isMinus) rx *= -1;
                    else ry *= -1;
                }

                ax = ax + rx;
                ay = ay + ry;

                if (ax < 0 || ax >= field.SizeX) return answer;
                if (ay >= field.SizeY) return answer;
                if (field.Cells[ax, ay].IsClosed) return answer;

                answer.nRP[i] = (rx, ry);
                answer.nAP[i] = (ax, ay);
            }

            answer.result = true;
            return answer;
        }
    }
}
