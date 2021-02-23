using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace BrickGame
{
    public partial class MainForm : Form
    {
        private Graphics drawField;
        private Field field;
        private PlayThread playThread;

        public MainForm()
        {
            InitializeComponent();

            drawField = pbField.CreateGraphics();
            field = new Field(14, 28);

            playThread = new PlayThread(field, drawField);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var t = new Thread(new ThreadStart(playThread.ThreadProc));
            t.Start();
        }

        private void MainForm_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (playThread == null) return;
            if (!playThread.IsActive) return;

            playThread.KeyPressed(e.KeyChar.ToString());
        }
    }
}
