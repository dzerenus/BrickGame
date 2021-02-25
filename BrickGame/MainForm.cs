using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace BrickGame
{
    public partial class MainForm : Form
    {
        private PlayThread playThread;

        public MainForm()
        {
            InitializeComponent();

            Graphics drawField = pbField.CreateGraphics();
            var bBrush = new SolidBrush(pbField.BackColor);
            playThread = new PlayThread(drawField, bBrush);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
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
