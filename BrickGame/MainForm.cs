using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace BrickGame
{
    public partial class MainForm : Form
    {
        private PlayThread playThread;

        Brush a1 = new SolidBrush(Color.FromArgb(255, 245, 217, 81));
        Brush a2 = new SolidBrush(Color.FromArgb(255, 255, 163, 71));
        Brush a3 = new SolidBrush(Color.FromArgb(255, 153, 78, 245));
        Brush a4 = new SolidBrush(Color.FromArgb(255, 78, 181, 245));
        Brush a5 = new SolidBrush(Color.FromArgb(255, 78, 245, 192));
        Brush a6 = new SolidBrush(Color.FromArgb(255, 150, 245, 78));
        Brush a7 = new SolidBrush(Color.FromArgb(255, 255, 112, 112));

        Brush d1 = new SolidBrush(Color.FromArgb(255, 10, 10, 10));
        Brush d2 = new SolidBrush(Color.FromArgb(255, 20, 20, 20));
        Brush d3 = new SolidBrush(Color.FromArgb(255, 30, 30, 30));
        Brush d4 = new SolidBrush(Color.FromArgb(255, 40, 40, 40));
        Brush d5 = new SolidBrush(Color.FromArgb(255, 50, 50, 50));
        Brush d6 = new SolidBrush(Color.FromArgb(255, 60, 60, 60));
        Brush d7 = new SolidBrush(Color.FromArgb(255, 70, 70, 70));

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (playThread != null) 
                playThread.IsActive = false;

            Brush[] aBrushes = new Brush[7] { a1, a2, a3, a4, a5, a6, a7 };
            Brush[] dBrushes = new Brush[7] { d1, d2, d3, d4, d5, d6, d7 };

            Graphics drawField = pbField.CreateGraphics();
            var bBrush = new SolidBrush(pbField.BackColor);
            playThread = new PlayThread(drawField, bBrush, aBrushes, dBrushes);

            var t = new Thread(new ThreadStart(playThread.ThreadProc));
            t.Start();
        }

        private void MainForm_KeyDown(object sender, KeyPressEventArgs e)
        {
            // Проверка: происходит ли игра в потоке.
            if (playThread == null) return;
            if (!playThread.IsActive) return;

            playThread.KeyPressed(e.KeyChar.ToString());
        }
    }
}
