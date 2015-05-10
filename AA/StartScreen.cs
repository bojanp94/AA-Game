using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AA
{
    public enum StartScreenState
    {
        Start,
        Won,
        Lost
    }

    public partial class StartScreen : Form
    {
        Timer timer;
        Form gameForm;

        public StartScreen()
        {
            InitializeComponent();
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += fadeOut;
            timer.Start();
            gameForm = new Game(this);
            gameForm.StartPosition = this.StartPosition;
            gameForm.Opacity = 0;
            gameForm.Show();
        }

        private void fadeOut(object sender, EventArgs e)
        {
            gameForm.Opacity += 0.05;
            if (gameForm.Opacity >= .95)
            {
                this.Opacity = 0;
                gameForm.Opacity = 1;
                timer.Stop();
            }
        }

        private void fadeIn(object sender, EventArgs e)
        {
            this.Opacity += 0.25;
            if (this.Opacity >= .95)
            {
                this.Opacity = 1;
                timer.Stop();
            }
        }

        public void reloadForm(StartScreenState state)
        {
            switch (state)
            {
                case StartScreenState.Won:
                    label1.Text = "Level " + (GameLevels.LoadCurrentLevel() + 1).ToString() + " won!";
                    button1.Text = "NEXT LEVEL";
                    this.BackColor = Color.Green;
                    break;
                case StartScreenState.Lost:
                    label1.Text = "Level " + (GameLevels.LoadCurrentLevel() + 1).ToString() + " lost!";
                    button1.Text = "TRY AGAIN";
                    this.BackColor = Color.Red;
                    break;
                case StartScreenState.Start:
                    label1.Text = "Level " + (GameLevels.LoadCurrentLevel() + 1).ToString() + " is next!";
                    button1.Text = "PLAY";
                    this.BackColor = Color.White;
                    break;
                default:
                    break;
            }
            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += fadeIn;          
            timer.Start();
        }
    }
}
