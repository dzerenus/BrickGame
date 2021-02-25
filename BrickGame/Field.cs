using System.Drawing;

namespace BrickGame
{
    class Field
    {   
        /// <summary>
        /// Двумерный массив клеток игрового поля.
        /// </summary>
        public Cell[,] Cells { get { return cells; } }
        private Cell[,] cells;

        /// <summary>
        /// Ширина игрового поля.
        /// </summary>
        public int SizeX { get { return sizeX; } }
        private int sizeX;

        /// <summary>
        /// Высота игрового поля.
        /// </summary>
        public int SizeY { get { return sizeY; } }
        private int sizeY;

        /// <summary>
        /// Фоновой цвет поля.
        /// </summary>
        public Color BackgroundColor 
        { 
            get { return backColor; } 
            set { backColor = value; } 
        }
        private Color backColor;

        /// <summary>
        /// Поле для рисования предметов.
        /// </summary>
        private Graphics graph;

        /// <summary>
        /// Класс игрового поля Тетриса.
        /// </summary>
        /// <param name="sX">Ширина поля.</param>
        /// <param name="sY">Высота поля.</param>
        /// <param name="bColor">Фоновой цвет игрового поля.</param>
        public Field(int sX, int sY, Color bColor)
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
