using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA
{
    public class Rotator
    {
        public double Angle { get; set; }
        public double Speed { get; set; }
        public int Radius { get; set; }
        public  const int BallAngle = 12;
        public const double DegToRad = Math.PI / 180.0;
        public  const int StickRatio = 3;
        public  const int Padding = 200;
        public List<Ball> Balls;
        Brush Brush; 


        public Rotator (double speed)
        {
            Angle = 0.0;
            Speed = speed;
            Radius = 50;
            Balls = new List<Ball>();           
            Brush = new SolidBrush (Color.Black);
            Ball.Radius = (int)(Radius * StickRatio * Math.Tan(BallAngle * DegToRad));
            

        }

        public void Draw (Graphics g)
        {
            g.FillEllipse(Brush, Padding, Padding, Radius * 2, Radius * 2);
            foreach (var ball in Balls)
            {
                var current_angle = ball.Angle;
                current_angle += Angle;
                current_angle %= 360.0;
                var coords = GetCoordinatesForBall(new Point(Padding + Radius, Padding + Radius), current_angle);
                g.FillEllipse(Brush, coords.X, coords.Y, Ball.Radius, Ball.Radius);

            }

        }
        public Point GetCoordinatesForBall(Point start, double angle)
        {
            var X = (int)(start.X + Math.Cos(angle * DegToRad) * Radius * StickRatio - Ball.Radius/2);
            var Y = (int)(start.X + Math.Sin(angle * DegToRad) * Radius * StickRatio - Ball.Radius/2);
            return new Point(X,Y);
        }

        

    }
}
