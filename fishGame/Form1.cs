using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fishGame
{

    public partial class frmGameScreen : Form
    {
        Button[,] btn = new Button[10, 10];
        Random rnd = new Random();
        System.Media.SoundPlayer splashSound = new System.Media.SoundPlayer(Properties.Resources.splash);
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

            // CHANGE SO ITS A YOINK AND TWIST
            string value = "";
            if (InputBox("Dialog Box", "What is your name?", ref value) == DialogResult.OK)
            {
                lblRedLabel.Text = value;
            }
            value = "";
            if (InputBox("Dialog Box", "What is your name?", ref value) == DialogResult.OK)
            {
                lblBlueLabel.Text = value;
            }
            firstTurns = true;
            redPlaying = true;
        }

        // NEED TO CHANGE IT ALL BECAUSE ITS FROM SOMEWHERE ELSE
        // proportions are terrible too
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            form.Text = title;
            label.Text = promptText;
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(36, 36, 372, 13);
            textBox.SetBounds(36, 86, 700, 20);
            buttonOk.SetBounds(228, 160, 160, 60);
            buttonCancel.SetBounds(400, 160, 160, 60);
            label.AutoSize = true;
            form.ClientSize = new Size(796, 307);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        void btnEvent_Click(object sender, EventArgs e)
        {
            string[] current = ((Button)sender).Name.Split(',');
            currentX = Int32.Parse(current[0]);
            currentY = Int32.Parse(current[1]);
            if (isGameOver(currentX, currentY))
            {
                gameOverBox();
            }
            if (redPlaying == false && bluePlaying == false)
            {
                if (((Button)sender).BackColor == Color.Red)
                {
                    redPlaying = true;
                    ((Button)sender).BackColor = Color.Black;
                    ((Button)sender).BackgroundImage = null;
                    splashSound.Play();
                    ((Button)sender).Text = "0";
                    prevTile = ((Button)sender).Name;
                }
                else if (((Button)sender).BackColor == Color.Blue)
                {
                    bluePlaying = true;
                    ((Button)sender).BackColor = Color.Black;
                    ((Button)sender).BackgroundImage = null;
                    splashSound.Play();
                    ((Button)sender).Text = "0";
                    prevTile = ((Button)sender).Name;
                }
            }
            else if (redPlaying == true)
            {
                string[] prev = prevTile.Split(',');
                prevX = Int32.Parse(prev[0]);
                prevY = Int32.Parse(prev[1]);

                current = ((Button)sender).Name.Split(',');
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
                        ((Button)sender).BackColor = Color.Red;
                        // change image to red penguin
                        ((Button)sender).BackgroundImage = fishGame.Properties.Resources.linux_penguin_red;
                        ((Button)sender).BackgroundImageLayout = ImageLayout.Center;
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

                current = ((Button)sender).Name.Split(',');
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
                        // change image to blue penguin
                        ((Button)sender).BackgroundImage = fishGame.Properties.Resources.linux_penguin_blue;
                        ((Button)sender).BackgroundImageLayout = ImageLayout.Center;
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

        private bool isGameOver(int x, int y)
        {
            bool gameOver = false;
            // check square x-1, x+1, y-1, y+1 colour, if none blue then cant move
            // need to fix for out of bounds array, prob just hard coding with ifs tbh
            if (btn[x - 1, y].BackColor != Color.PowderBlue && btn[x + 1, y].BackColor != Color.PowderBlue && btn[x, y - 1].BackColor != Color.PowderBlue && btn[x, y + 1].BackColor != Color.PowderBlue)
            {
                gameOver = true;
            }
            return gameOver;
        }

        private void gameOverBox()
        {
            // make this congratulate winner, give start again option or close option
            DialogResult result;
            result = MessageBox.Show("Game Complete!", "Game Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
