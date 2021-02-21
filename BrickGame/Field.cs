using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickGame
{
    class Field
    {   
        // Все клетки поля.
        public Cell[,] Cells { get { return cells; } }
        private Cell[,] cells;

        // Ширина игрового поля.
        public int SizeX { get { return sizeX; } }
        private int sizeX;

        // Ширина игрового поля.
        public int SizeY { get { return sizeY; } }
        private int sizeY;

        public Field(int sX, int sY)
        {
            sizeX = sX;
            sizeY = sY;

            // Создаём клетки поля.
            cells = new Cell[sX,sY];

            // Заполняем ссылки на клетки экземплярами.
            for (int i = 0; i < sX; i++)
                for (int j = 0; j < sY; j++)
                    cells[i, j] = new Cell(i, j);
        }
    }
}
