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
        public int HorizontalOffset { get; set; }
        public const int BallAngle = 8;
        public const double DegToRad = Math.PI / 180.0;
        public const int StickRatio = 3;
        public const int Padding = 250;
        public const int PenWidth = 2;
        public List<Ball> Balls;
        Brush Brush;
        Pen Pen;
        public int NumberOfBallsToShoot { get; set; }
        public int NumberOfBallsRotator { get; set; }
        public bool RotateClockWise { get; set; }


        public Rotator (double speed)
        {
            Angle = 0.0;
            Speed = speed;
            Radius = 80;
            HorizontalOffset = 0;
            Balls = new List<Ball>();           
            Brush = new SolidBrush (Color.Black);
            Pen = new Pen(Brush, PenWidth);
            Ball.Radius = (int)(Radius * StickRatio * Math.Tan(BallAngle * DegToRad));
            RotateClockWise = true;
        }

        public void Draw (Graphics g)
        {
            // draw the main circle
            g.FillEllipse(Brush, Padding + HorizontalOffset, Padding, Radius * 2, Radius * 2);
            foreach (var ball in Balls)
            {
                var current_angle = ball.Angle;
                current_angle += Angle;
                current_angle %= 360.0;
                var center = new Point(Padding + Radius + HorizontalOffset, Padding + Radius);
                var coords = GetCoordinatesForBall(center, current_angle, Ball.Radius/2);

                // draw the ball
                g.FillEllipse(Brush, coords.X, coords.Y, Ball.Radius, Ball.Radius);

                // draw the stick
                var line_coords = GetCoordinatesForBall(center, current_angle, 0);
                g.DrawLine(Pen, center, line_coords);
            }

            // draw the waiting ball
            g.FillEllipse(Brush, Padding + Radius + HorizontalOffset - Ball.Radius / 2, Padding + Radius * 2 + Radius * StickRatio , Ball.Radius, Ball.Radius);
            if (NumberOfBallsToShoot / 10 > 0)
            {
                g.DrawString(NumberOfBallsToShoot.ToString(), new Font(new FontFamily("Arial"), 12), new SolidBrush(Color.White), 5 + Padding + Radius + HorizontalOffset - Ball.Radius / 2, 8 + Padding + Radius * 2 + Radius * StickRatio);
            }
            else
            {
                g.DrawString(NumberOfBallsToShoot.ToString(), new Font(new FontFamily("Arial"), 12), new SolidBrush(Color.White), 10 + Padding + Radius + HorizontalOffset - Ball.Radius / 2, 8 + Padding + Radius * 2 + Radius * StickRatio);
            }
        }
        public Point GetCoordinatesForBall(Point start, double angle, int offset)
        {
            var X = (int)(start.X + Math.Cos(angle * DegToRad) * Radius * StickRatio - offset);
            var Y = (int)(start.Y + Math.Sin(angle * DegToRad) * Radius * StickRatio - offset);
            return new Point(X,Y);
        }

        public void Rotate()
        {
            if (RotateClockWise)
            {
                Angle = (Angle + Speed) % 360;
            }
            else
            {
                Angle = (Angle - Speed) % 360;
            }
        }

        public void AddNewBall()
        {
            if (NumberOfBallsToShoot == 0)
            {
                // you win message
                return;
            }

            NumberOfBallsToShoot--;

            var angle = 90 - Angle;
            if (angle < 0) {
                angle += 360;
            }
            Balls.Add(new Ball(angle));
        }

        public bool CheckForCollision()
        {
            var last_ball = Balls[Balls.Count - 1];

            for (int i = 0; i < Balls.Count - 1; i++)
            {
                if (Math.Abs(last_ball.Angle - Balls[i].Angle) < BallAngle || Math.Abs(last_ball.Angle - Balls[i].Angle) > (360 - BallAngle))
                    return true;
            }

            return false;
        }
    }
}
