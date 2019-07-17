using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LedController
{
    public partial class Visualizer : Form
    {
        private const int sqrSize = 50;
        public readonly int start;
        public readonly int width;
        public readonly int height;
        public readonly bool cw;
        private LedMatrix matrix;
        Graphics g;

        private bool busy = true;

        public Visualizer(LedMatrix m)
        {
            InitializeComponent();
            matrix = m;
            start = matrix.Start;
            width = matrix.Width;
            height = matrix.Height;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public void Update(CColor[] colors)
        {
            if (!busy)
            {
                matrix.AssignFrom(matrix.Start, colors);
                CColor[] relColors = matrix.ReturnRelativeColors();
                SolidBrush b = new SolidBrush(new Color());

                int x = 0;
                int y = 0;
                int i = 0;
                do
                {
                    x++;
                    b.Color = relColors[i++].ToColor();
                    g.FillRectangle(b, new Rectangle(x * sqrSize + 12, y * sqrSize + 12, sqrSize, sqrSize));                 
                } while (x < width - 1);
                do
                {
                    y++;
                    b.Color = relColors[i++].ToColor();
                    g.FillRectangle(b, new Rectangle(x * sqrSize + 12, y * sqrSize + 12, sqrSize, sqrSize));
                } while (y < height - 1);
                do
                {
                    x--;
                    b.Color = relColors[i++].ToColor();
                    g.FillRectangle(b, new Rectangle(x * sqrSize + 12, y * sqrSize + 12, sqrSize, sqrSize));
                } while (x > 0);
                do
                {
                    y--;
                    b.Color = relColors[i++].ToColor();
                    g.FillRectangle(b, new Rectangle(x * sqrSize + 12, y * sqrSize + 12, sqrSize, sqrSize));
                } while (y > 0);
                b.Dispose();
            }
        }

        private void Visualizer_Load(object sender, EventArgs e)
        {
            statLabel.Location = new Point(24 + sqrSize, 24 + sqrSize);
            statLabel.Text = $"Number of leds: {matrix.MasterLength}\nWidth: {matrix.Width}\nHeight: {matrix.Height}\nStarting Index: {matrix.Start}\n{(matrix.IsCw ? "Clockwise" : "Counter-clockwise")}";
            ClientSize = new Size
                (
                width * sqrSize + 24,
                height * sqrSize + 24
                );
            g = CreateGraphics();
            busy = false;
        }

        private void Visualizer_FormClosing(object sender, FormClosingEventArgs e)
        {
            busy = true;
            g.Dispose();
        }
    }
}
