using System.Drawing;

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

        /// <summary>
        /// Класс игрового поля Тетриса.
        /// </summary>
        /// <param name="sX">Ширина поля.</param>
        /// <param name="sY">Высота поля.</param>
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

        /// <summary>
        /// Обновление всех клеток игрового поля.
        /// </summary>
        /// <param name="clr">Кисть фонового цвета.</param>
        /// <param name="graph">Поле для рисования.</param>
        public void Update(Graphics graph, Brush clr)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i / 10, i % 10].IsClosed) break;
                if (cells[i / 10, i % 10].IsFill) break;

                else
                    cells[i / 10, i % 10].Fill(graph, clr);
            }
        }
    }
}
