using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fishGame
{
    public partial class frmGameScreen : Form
    {
        Button[,] btn = new Button[10, 10];
        Random rnd = new Random();
        int num = 0;
        bool redPlaying = false;
        bool bluePlaying = false;
        int redPoints = 0;
        int bluePoints = 0;
        bool firstTurns = false;
        string prevTile = "-1,-1";
        int prevX = -1;
        int prevY = -1;
        int currentX = -1;
        int currentY = -1;
        bool obstruction = false;

        public frmGameScreen()
        {
            InitializeComponent();
            for (int i = 0; i < btn.GetLength(0); i++) 
            {
                for (int j = 0; j < btn.GetLength(1); j++)
                {
                    btn[i, j] = new Button();
                    btn[i, j].SetBounds(100 + (45 * i), 25 + (45 * j), 45, 45);
                    btn[i, j].BackColor = Color.PowderBlue;
                    btn[i, j].Text = Convert.ToString(num = rnd.Next(1, 4));
                    btn[i, j].Name = Convert.ToString(i + "," + j);
                    btn[i, j].Click += new EventHandler(this.btnEvent_Click);
                    Controls.Add(btn[i, j]);
                }
            }

            DialogResult result;
            result = MessageBox.Show("Red plays first! Click a box to make your first move.", "Red First Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firstTurns = true;
            redPlaying = true;
        }
        void btnEvent_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(((Button)sender).Name);





            if (redPlaying == false && bluePlaying == false)
            {
                if (((Button)sender).BackColor == Color.Red)
                {
                    redPlaying = true;
                    ((Button)sender).BackColor = Color.Black;
                    ((Button)sender).Text = "0";
                    prevTile = ((Button)sender).Name;
                }
                else if (((Button)sender).BackColor == Color.Blue)
                {
                    bluePlaying = true;
                    ((Button)sender).BackColor = Color.Black;
                    ((Button)sender).Text = "0";
                    prevTile = ((Button)sender).Name;
                }
            }
            else if (redPlaying == true) //blue false?
            {
                string[] prev = prevTile.Split(',');
                prevX = Int32.Parse(prev[0]);
                prevY = Int32.Parse(prev[1]);

                string[] current = ((Button)sender).Name.Split(',');
                currentX = Int32.Parse(current[0]);
                currentY = Int32.Parse(current[1]);

                if ((currentX == prevX || currentY == prevY) || firstTurns == true)
                {
                    if (currentX == prevX)
                    {
                        for (int i = 1; i< Math.Abs(prevY - currentY); i++)
                        {
                            if (prevY < currentY)
                            {
                                if (btn[currentX, (i + prevY)].BackColor != Color.PowderBlue)
                                    obstruction = true;
                            }

                            else if (prevY > currentY)
                            {
                                if (btn[currentX, (i + currentY)].BackColor != Color.PowderBlue)
                                    obstruction = true;
                            }
                        }
                    }

                    if (currentY == prevY)
                    {
                        for (int i = 1; i < Math.Abs(prevX - currentX); i++)
                        {
                            if (prevX < currentX)
                            {
                                if (btn[(i + prevX), currentY].BackColor != Color.PowderBlue)
                                    obstruction = true;
                            }

                            else if (prevX > currentX)
                            {
                                if (btn[(i + currentX), currentY].BackColor != Color.PowderBlue)
                                    obstruction = true;
                            }
                        }
                    }

                    if ((((Button)sender).BackColor == Color.PowderBlue) && obstruction == false)
                    {
                        ((Button)sender).BackColor = Color.Red;
                        num = Int32.Parse(((Button)sender).Text);
                        redPoints += num;
                        ((Label)lblRedPoints).Text = redPoints.ToString();
                        redPlaying = false;

                        if (firstTurns == true)
                            bluePlaying = true;
                    }
                }
            }
            else if (bluePlaying == true)
            {
                string[] prev = prevTile.Split(',');
                prevX = Int32.Parse(prev[0]);
                prevY = Int32.Parse(prev[1]);

                string[] current = ((Button)sender).Name.Split(',');
                currentX = Int32.Parse(current[0]);
                currentY = Int32.Parse(current[1]);

                if ((currentX == prevX || currentY == prevY) || firstTurns == true)
                {
                    if (currentX == prevX)
                    {
                        for (int i = 1; i < Math.Abs(prevY - currentY); i++)
                        {
                            if (prevY < currentY)
                            {
                                if (btn[currentX, (i + prevY)].BackColor != Color.PowderBlue)
                                    obstruction = true;
                            }

                            else if (prevY > currentY)
                            {
                                if (btn[currentX, (i + currentY)].BackColor != Color.PowderBlue)
                                    obstruction = true;
                            }
                        }
                    }

                    if (currentY == prevY)
                    {
                        for (int i = 1; i < Math.Abs(prevX - currentX); i++)
                        {
                            if (prevX < currentX)
                            {
                                if (btn[(i + prevX), currentY].BackColor != Color.PowderBlue)
                                    obstruction = true;
                            }

                            else if (prevX > currentX)
                            {
                                if (btn[(i + currentX), currentY].BackColor != Color.PowderBlue)
                                    obstruction = true;
                            }
                        }
                    }

                    if ((((Button)sender).BackColor == Color.PowderBlue) && obstruction == false)
                    {
                        ((Button)sender).BackColor = Color.Blue;
                        num = Int32.Parse(((Button)sender).Text);
                        bluePoints += num;
                        ((Label)lblBluePoints).Text = bluePoints.ToString();
                        bluePlaying = false;

                        if (firstTurns == true)
                            firstTurns = false;
                    }
                }
            }

            obstruction = false;
        }

            private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Penguin Game in C# by Emma Martin, Martyn Bett and Lucy Thomson (c) 2024", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmGameScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
