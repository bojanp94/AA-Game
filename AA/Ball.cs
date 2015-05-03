using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AA
{
    public class Ball
    {
        public static int Radius { get; set; }
        public double Angle { get; set; }
        public Ball (double angle)
        {
            Angle = angle;
        }
    }
}
