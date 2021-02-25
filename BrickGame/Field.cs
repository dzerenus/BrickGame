using System.Drawing;
using System.Collections.Generic;

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
        /// Поле для рисования предметов.
        /// </summary>
        private Graphics graph;

        /// <summary>
        /// Класс игрового поля Тетриса.
        /// </summary>
        /// <param name="sX">Ширина поля.</param>
        /// <param name="sY">Высота поля.</param>
        /// <param name="backBrush">Фоновой цвет игрового поля.</param>
        public Field(int sX, int sY, Brush backBrush, Graphics grph)
        {
            // Создаём ручку для рисования границ.
            Pen brdPen = new Pen(Brushes.Black);

            graph = grph;

            sizeX = sX;
            sizeY = sY;

            // Создаём клетки поля.
            cells = new Cell[sX,sY];

            // Заполняем ссылки на клетки экземплярами.
            for (int i = 0; i < sX; i++)
                for (int j = 0; j < sY; j++)
                    cells[i, j] = new Cell(i, j, brdPen, backBrush, grph);
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
                    cells[i / 10, i % 10].Fill();
            }
        }

        /// <summary>
        /// Функция, анализирующая поле на количество заполненных линий.
        /// </summary>
        /// <returns>Список номеров строк заполненных линий.</returns>
        public List<int> LinesAnalize()
        {
            List<int> numLines = new List<int>();

            for (int y = sizeY - 1; y >= 0; y--)
            {
                // Счётчик закрытых ячеек в линии.
                var cellCounter = 0;

                for (int x = sizeX - 1; x >= 0; x--)
                {
                    if (cells[x, y].IsClosed) cellCounter++;
                    else break;
                }

                // Если число закрытых ячеек равно ширине линии, отмечаем линию к удалению.
                if (cellCounter == sizeX) numLines.Add(y);
            }

            return numLines;
        }

        /// <summary>
        /// Процедура удаления линий с поля. Убирает необходимые линии и смещает линии выше вниз.
        /// </summary>
        /// <param name="lines">Список номеров лиинй к удалению.</param>
        public void RemoveLines(List<int> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                var y = lines[i] + i;

                // Цикл стирания линий.
                for (int x = 0; x < sizeX; x++)
                {
                    cells[x, y].Fill();
                    cells[x, y].IsClosed = false;
                }

                // Цикл смещения остальных линий ниже.
                for (int u = y - 1; u > 0; u--)
                {
                    for (int x = 0; x < sizeX; x++)
                    {
                        if (cells[x, u].IsClosed)
                        {
                            cells[x, u + 1].Draw(cells[x, u].CellColor);
                            cells[x, u + 1].IsClosed = true;

                            cells[x, u].IsClosed = false;
                            cells[x, u].Fill();
                        }
                    }
                }
            }
        }
    }
}
