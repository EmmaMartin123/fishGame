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
        bool muteSound = false;

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
            
            string value = "";
            if (InputBox("Choose your display name", "Enter your chosen display name", ref value,9) == DialogResult.OK)
            {
                txtRed.Text = value;
            }
            value = "";
            if (InputBox("Choose your display name", "Enter your chosen display name", ref value,9) == DialogResult.OK)
            {
                txtBlue.Text = value;
            }
            firstTurns = true;
            redPlaying = true;
            // tell player who's first and what colour
            DialogResult result;
            result = MessageBox.Show(txtRed.Text + " plays first! You are red. Click a box to make your first move. \n" + txtBlue.Text + " will play second. You are blue.", "First Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // NEED TO CHANGE IT ALL BECAUSE ITS FROM SOMEWHERE ELSE
        // proportions are terrible too
        public static DialogResult InputBox(string title, string promptText, ref string value, int maxLength)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            form.Text = title;
            label.Text = promptText;
            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            label.SetBounds(36, 36, 372, 13);
            textBox.SetBounds(36, 86, 700, 20);
            buttonOk.SetBounds(228, 160, 160, 60);
            label.AutoSize = true;
            form.ClientSize = new Size(796, 307);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk });
            form.AcceptButton = buttonOk;
            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;

            while (value.Length > maxLength)
            {
                MessageBox.Show("Please enter a valid name with 1 to " + maxLength + " characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dialogResult = form.ShowDialog();
                value = textBox.Text;
            }
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
                    if(muteSound == false)
                    {
                        splashSound.Play();
                    }
                    ((Button)sender).Text = "0";
                    prevTile = ((Button)sender).Name;
                }
                else if (((Button)sender).BackColor == Color.Blue)
                {
                    bluePlaying = true;
                    ((Button)sender).BackColor = Color.Black;
                    ((Button)sender).BackgroundImage = null;
                    if (muteSound == false)
                    {
                        splashSound.Play();
                    }
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
            bool gameOver = true;
            // can move left
            if (x-1>=0 && btn[x - 1, y].BackColor == Color.PowderBlue)
                return gameOver = false;
            //can move right
            if (x + 1 <=14 && btn[x + 1, y].BackColor == Color.PowderBlue)
                return gameOver = false;
            // can move up
            if (y-1 >= 0 && btn[x,y-1].BackColor == Color.PowderBlue)
                return gameOver = false;
            // can move down
            if (y + 1 <= 14 && btn[x, y + 1].BackColor == Color.PowderBlue)
                return gameOver = false;

            return gameOver;
        }

        private void gameOverBox()
        {
            DialogResult result;
            if (redPoints > bluePoints)
            {
                result = MessageBox.Show("Game Complete! Winner is " + txtRed.Text + " with " + redPoints + " points. Would you like to play again?", "Game Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
            else
            {
                result = MessageBox.Show("Game Complete! Winner is " + txtBlue.Text + " with " + bluePoints + " points. Would you like to play again?", "Game Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
            if (result == DialogResult.Yes)
            {
                // Code to start a new game
            }
            else
            {
                Close();
            }
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

        private void txtRed_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBlue_TextChanged(object sender, EventArgs e)
        {

        }

        private void cgbxbMute_CheckedChanged(object sender, EventArgs e)
        {
            if (muteSound == false)
            {
                muteSound = true;
            }
            else
            {
                muteSound = false;
            }
        }
    }
}
