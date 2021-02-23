using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickGame
{
    class Figure
    {
        /// <summary>
        /// Заблокирована ли фигура.
        /// </summary>
        public bool IsClosed = false;

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

        /// <summary>
        /// Класс, использующийся, для создания и отрисовки фигур в момент их падения на поле.
        /// </summary>
        /// <param name="type">Тип фигуры. От 0 до 6.</param>
        /// <param name="xSpawnPosition">Координата появления фигуры на поле по X.</param>
        public Figure(int type, int xSpawnPosition=6)
        {
            switch (type)
            {
                // Если фигура - палка.
                case 0:
                    rPosition[0] = (0, 1);
                    rPosition[1] = (0, 0);
                    rPosition[2] = (0, -1);
                    rPosition[3] = (0, -2);

                    aPosition[0] = (xSpawnPosition, rPosition[0].y);
                    aPosition[1] = (xSpawnPosition, rPosition[1].y);
                    aPosition[2] = (xSpawnPosition, rPosition[2].y);
                    aPosition[3] = (xSpawnPosition, rPosition[3].y);

                    break;

                // Если фигура - трезубец.
                case 1:
                    rPosition[0] = (0, 0);
                    rPosition[1] = (0, -1);
                    rPosition[2] = (-1, 0);
                    rPosition[3] = (1, 0);

                    aPosition[0] = (xSpawnPosition + rPosition[0].x, rPosition[0].y);
                    aPosition[1] = (xSpawnPosition + rPosition[1].x, rPosition[1].y);
                    aPosition[2] = (xSpawnPosition + rPosition[2].x, rPosition[2].y);
                    aPosition[3] = (xSpawnPosition + rPosition[3].x, rPosition[3].y);
                    break;

                // Если фигура - квадрат.
                case 2: break;

                // Если фигура - Z.
                case 3: break;

                // Если фигура - обратная Z.
                case 4: break;

                // Если фигура - L.
                case 5: break;

                // Если фигура - обратная L.
                case 6: break;
                
                // Если тип фигуры задан ошибочно.
                default: throw new Exception("Неизвестный тип фигуры");
            }
        }
    }
}
