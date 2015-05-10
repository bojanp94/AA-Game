using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AA
{
    public partial class Game : Form
    {
        Rotator Rotator;
        StartScreen parentForm;
        Timer closeTimer;
        GameLevels Levels;

        public Game()
        {
            InitializeComponent();
            DoubleBuffered = true;
            //Rotator = new Rotator(1);
            //Rotator.NumberOfBalls = 2;
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;

            this.BackColor = Color.White;
            Levels = new GameLevels();
            Rotator = Levels.GetLevel();
            Rotator.HorizontalOffset = (this.Width / 2) - Rotator.Padding - Rotator.Radius;
            KeyDown += Game_KeyDown;

            //for (int i = 0; i < 10; i++)
            //{
            //    Rotator.Balls.Add(new Ball(i * (360 / 10)));
            //}
        }

        public Game(StartScreen parent)
            : this()
        {
            parentForm = parent;
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            Rotator.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Rotator.Rotate();
            Invalidate();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Rotator.AddNewBall();
                if (Rotator.CheckForCollision())
                {
                    LoseGame();
                    Invalidate();
                }
                if (Rotator.NumberOfBallsToShoot == 0)
                {
                    WinLevel();
                    Invalidate();
                }
            }
        }

        private void LoseGame()
        {
            timer1.Stop();
            this.BackColor = Color.Red;
            Invalidate();
            FadeOutAndClose(StartScreenState.Lost);
        }

        private void FadeOutAndClose(StartScreenState state = StartScreenState.Start)
        {
            closeTimer = new Timer();
            closeTimer.Interval = 50;
            closeTimer.Tick += fadeOutTick;
            closeTimer.Start();
            parentForm.reloadForm(state);
        }

        private void fadeOutTick(object sender, EventArgs e)
        {
            this.Opacity -= 0.05;
            if (this.Opacity <= 0.05)
            {
                closeTimer.Stop();
                this.Close();
            }
        }

        private void NextLevel()
        {
            //closeTimer = new Timer();
            //closeTimer.Interval = 50;
            //closeTimer.Tick += fadeOutTick;
            //closeTimer.Start();

            //if (Levels.NextLevel() != null)
            //{
                Rotator = Levels.NextLevel();
                Rotator.HorizontalOffset = (this.Width / 2) - Rotator.Padding - Rotator.Radius;
            //}
            //else
            //{
            //    // you win message
            //    this.Close();
            //}

            //this.BackColor = Color.White;
            //timer1.Start();
            //Invalidate();
        }

        private void WinLevel()
        {
            timer1.Stop();
            this.BackColor = Color.Green;
            Invalidate();
            FadeOutAndClose(StartScreenState.Won);
            NextLevel();
        }

        private void Game_SizeChanged(object sender, EventArgs e)
        {
            Rotator.HorizontalOffset = (this.Width / 2) - Rotator.Padding - Rotator.Radius;
        }
    }
}
