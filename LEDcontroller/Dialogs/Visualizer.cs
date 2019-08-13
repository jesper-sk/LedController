using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LedController
{
    public partial class Visualizer : Form
    {
        public const int SqrSize = 50;
        int width, height;
        Graphics e;


        public Visualizer(int mw, int mh)
        {
            InitializeComponent();
            width = mw;
            height = mh;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public void FillRect(CColor c, int x, int y)
        {
            using(SolidBrush b = new SolidBrush(c.ToColor()))
            {
                e.FillRectangle(b, 12 + x, 12 + y, SqrSize, SqrSize);
            }
        }

        private void Visualizer_Load(object sender, EventArgs ea)
        {
            ClientSize = new Size
                (
                width * SqrSize + 24,
                height * SqrSize + 24
                );
            e = CreateGraphics();
        }

        private void Visualizer_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public new void Dispose()
        {
            e.Dispose();
        }

        ~Visualizer()
        {
            e.Dispose();
        }
    }
}
