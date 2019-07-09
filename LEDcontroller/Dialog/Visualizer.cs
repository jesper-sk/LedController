using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedController
{
    public partial class Visualizer : Form
    {
        private const int buttWidth = 50;
        private const int buttHeight = buttWidth;
        public readonly int start;
        public readonly int width;
        public readonly int height;
        public readonly bool cw;
        private List<Button> Buttons;
        private LedMatrix Matrix;

        private bool init = true;

        public Visualizer(LedMatrix m)
        {
            InitializeComponent();
            Matrix = m;
            start = Matrix.Start;
            width = Matrix.Width;
            height = Matrix.Height;
        }

        public void Update(CColor[] colors)
        {
            if (!init)
            {
                Matrix.AssignFrom(Matrix.Start, colors);
                CColor[] absColorsList = Matrix.ReturnRelativeColors();
                for (int i = 0; i < Buttons.Count; i++)
                {
                    Buttons[i].BackColor = absColorsList[i];
                }
            }
        }

        private void Visualizer_Load(object sender, EventArgs e)
        {
            Buttons = new List<Button>();
            int x = 0;
            int y = 0;
            do
            {
                x++;
                Button butt = new Button()
                {
                    Location = new Point(x * buttWidth + 12, y * buttHeight + 12),
                    Size = new Size(buttWidth, buttHeight),
                    Text = "",
                    BackColor = Color.Black
                };
                Buttons.Add(butt);
                Controls.Add(butt);
            } while (x < width - 1);
            do
            {
                y++;
                Button butt = new Button()
                {
                    Location = new Point(x * buttWidth + 12, y * buttHeight + 12),
                    Size = new Size(buttWidth, buttHeight),
                    Text = "",
                    BackColor = Color.Black
                };
                Buttons.Add(butt);
                Controls.Add(butt);
            } while (y < height - 1);
            do
            {
                x--;
                Button butt = new Button()
                {
                    Location = new Point(x * buttWidth + 12, y * buttHeight + 12),
                    Size = new Size(buttWidth, buttHeight),
                    Text = "",
                    BackColor = Color.Black
                };
                Buttons.Add(butt);
                Controls.Add(butt);
            } while (x > 0);
            do
            {
                y--;
                Button butt = new Button()
                {
                    Location = new Point(x * buttWidth + 12, y * buttHeight + 12),
                    Size = new Size(buttWidth, buttHeight),
                    Text = "",
                    BackColor = Color.Black
                };
                Buttons.Add(butt);
                Controls.Add(butt);
            } while (y > 0);

            Size = new Size
                (
                width * buttWidth + 100,
                height * buttHeight + 100
                );

            init = false;
        }
    }
}
