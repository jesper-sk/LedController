using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LedController
{
    public partial class Visualizer : Form
    {
        public int SqrSize { get; private set; } = 50;
        int width, height;
        Graphics e;
        int paddingX, paddingY;

        public Visualizer(int mw, int mh)
        {
            InitializeComponent();
            width = mw;
            height = mh;
            paddingX = paddingY = 12;
        }

        public void FillRect(CColor c, int x, int y)
        {
            using (SolidBrush b = new SolidBrush(c.ToColor()))
            {
                e.FillRectangle(b, paddingX + x, paddingY + y, SqrSize, SqrSize);
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

        private void Visualizer_Resize(object sender, EventArgs _)
        {
            SqrSize = Math.Min((ClientSize.Width - 12) / width, (ClientSize.Height - 12) / height);
            paddingX = (ClientSize.Width - (SqrSize * width)) / 2;
            paddingY = (ClientSize.Height - (SqrSize * height)) / 2;

            e = CreateGraphics();
        }

        ~Visualizer()
        {
            e.Dispose();
        }
    }
}
