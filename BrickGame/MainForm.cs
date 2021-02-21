using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BrickGame
{
    public partial class MainForm : Form
    {
        private Graphics drawField;
        private Field field;

        public MainForm()
        {
            InitializeComponent();
            drawField = pbField.CreateGraphics();
            field = new Field(14, 28);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var pt = new PlayThread(field, drawField);

            var t = new Thread(new ThreadStart(pt.ThreadProc));
            t.Start();
        }

    }
}
