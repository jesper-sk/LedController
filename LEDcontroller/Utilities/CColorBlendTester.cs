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

namespace LedController
{
    public partial class CCColorBlendTester : Form
    {
        CColor start;
        CColor end;
        int done = 0;
        public CCColorBlendTester()
        {
            InitializeComponent();
            start = CColor.FromRgb(0, 0, 192);
            end = CColor.FromRgb(234, 45, 0);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Graphics formGraphics;
            formGraphics = CreateGraphics();

            switch (done)
            {
                case 0:
                    {
                        SolidBrush brush = new SolidBrush(start.ToColor());
                        formGraphics.FillEllipse(brush, new Rectangle(1, 1, 100, 100));
                        Thread.Sleep(1000);
                        done = 1;
                        for (double t = 0.01; t <= 1; t += 0.01)
                        {
                            brush.Color = CColor.Blend(start, end, t).ToColor();
                            formGraphics.FillEllipse(brush, new Rectangle(1, 1, 100, 100));
                            Thread.Sleep(10);
                        }
                        brush.Dispose();
                        done = 2;
                        Thread.Sleep(100);
                        break;
                    }
            }
            formGraphics.Dispose();
            DialogResult = DialogResult.OK;
        }
    }
}
