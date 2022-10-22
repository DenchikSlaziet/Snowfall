using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snow
{
    public partial class Form1 : Form
        {
            private readonly IList<Snow> snows;
            private readonly Timer timer;
            Bitmap background;
            Bitmap snow;
            int Counter = 0;
            Bitmap scene;
            Bitmap mainbitmap;
            private Graphics dopa;

            public Form1()
            {
                InitializeComponent();
                snows = new List<Snow>();
                background = new Bitmap(Properties.Resources._3);
                snow = new Bitmap(Properties.Resources._1);
                scene = new Bitmap(background, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                mainbitmap = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                dopa = Graphics.FromImage(mainbitmap);
                CreateSnow();
                timer = new Timer();
                timer.Interval = 50;
                timer.Tick += Timer_Tick;
                
            }

            private void Timer_Tick(object sender, EventArgs e)
            {
            timer.Stop();
                //Print();
            foreach (var snowflake in snows)
                {
                snowflake.Y += snowflake.Razmer;
                if (snowflake.Y > Screen.PrimaryScreen.WorkingArea.Height)
                    {
                        snowflake.Y = -snowflake.Razmer;
                        
                    }
                 }
            Print();
            timer.Start();
        }

            private void Form1_Paint(object sender, PaintEventArgs e)
            {
            BackgroundImage = scene;
            }

            private void CreateSnow()
            {
                var rnd = new Random();
                for (int i = 0; i < 60; i++)
                {
                snows.Add(new Snow
                {
                    X = rnd.Next(Screen.PrimaryScreen.WorkingArea.Width),
                    Y = -rnd.Next(Screen.PrimaryScreen.WorkingArea.Height),
                    Razmer = rnd.Next(5, 35)
                }) ;
                }
            }

        private void Print()
        {
            dopa.DrawImage(scene, new Rectangle(0, 0, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height));
            foreach (var snowflake in snows)
            {
                if (snowflake.Y > 0)
                {
                    dopa.DrawImage(snow, new Rectangle(
                        snowflake.X,
                        snowflake.Y,
                        snowflake.Razmer,
                        snowflake.Razmer));
                }
            }
            var g = CreateGraphics();
            g.DrawImage(mainbitmap, 0, 0);

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if(Counter==1)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
                Counter = 0;
            }
            ++Counter;
        }
    }
    }

