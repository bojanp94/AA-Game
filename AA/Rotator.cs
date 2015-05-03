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
        public const int BallAngle = 8;
        public const double DegToRad = Math.PI / 180.0;
        public const int StickRatio = 3;
        public const int Padding = 250;
        public const int PenWidth = 2;
        public List<Ball> Balls;
        Brush Brush;
        Pen Pen;


        public Rotator (double speed)
        {
            Angle = 0.0;
            Speed = speed;
            Radius = 80;
            Balls = new List<Ball>();           
            Brush = new SolidBrush (Color.Black);
            Pen = new Pen(Brush, PenWidth);
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
                var center = new Point(Padding + Radius, Padding + Radius);
                var coords = GetCoordinatesForBall(center, current_angle, Ball.Radius/2);

                // draw the ball
                g.FillEllipse(Brush, coords.X, coords.Y, Ball.Radius, Ball.Radius);

                // draw the stick
                var line_coords = GetCoordinatesForBall(center, current_angle, 0);
                g.DrawLine(Pen, center, line_coords);
            }

        }
        public Point GetCoordinatesForBall(Point start, double angle, int offset)
        {
            var X = (int)(start.X + Math.Cos(angle * DegToRad) * Radius * StickRatio - offset);
            var Y = (int)(start.X + Math.Sin(angle * DegToRad) * Radius * StickRatio - offset);
            return new Point(X,Y);
        }



        public void Rotate()
        {
            Angle = (Angle + Speed) % 360;
        }
    }
}
