using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AA
{
    public partial class Form1 : Form
    {
        Rotator Rotator;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Rotator = new Rotator(1);
            //Rotator.Balls.Add(new Ball(0));
            //Rotator.Balls.Add(new Ball(90));
            //Rotator.Balls.Add(new Ball(180));
            //Rotator.Balls.Add(new Ball(270));

            for (int i = 0; i < 10; i++)
            {
                Rotator.Balls.Add(new Ball(i*(360/10)));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rotator.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Rotator.Rotate();
            Invalidate();
        }
    }
}
