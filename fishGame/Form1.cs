using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * AC22005 - Computer Systems 2B: Architecture and Operating Systems ( SEM 2 23/24 )
 * Assessment 1
 * Group 2: Martyn Bett [2508672], Emma Martin [2502836], Lucy Thomson [2505312]
 */

namespace fishGame
{

    public partial class frmGameScreen : Form
    {
        // Initialise global variables...

        // Button grid
        Button[,] btn = new Button[10, 10];

        // Random value for tile fish quantities 
        Random rnd = new Random();

        // Splash sound for when penguins are moved
        System.Media.SoundPlayer splashSound = new System.Media.SoundPlayer(Properties.Resources.splash);

        // Integer value for tile fish quantities
        int num = 0;

        // Checks for which player is currently playing
        bool redPlaying = false;
        bool bluePlaying = false;

        // Point totals for each colour
        int redPoints = 0;
        int bluePoints = 0;

        // Flag for whether or not the current turn is the first one
        bool firstTurns = false;

        // Keeps track of the penguin's position - for checking valid moves
        string prevTile = "-1,-1";
        int prevX = -1;
        int prevY = -1;
        int currentX = -1;
        int currentY = -1;

        // For checking valid moves
        bool obstruction = false;

        // For muting the sound effect
        bool muteSound = false;

        // For turn-based play
        char lastPlayed = '\0';
        
        // Finding current directory
        string highScoresFile = Directory.GetCurrentDirectory() + "\\HighScore.txt";

        public frmGameScreen()
        {
            InitializeComponent();

            // Create grid of buttons
            for (int i = 0; i < btn.GetLength(0); i++)
            {
                for (int j = 0; j < btn.GetLength(1); j++)
                {
                    btn[i, j] = new Button();
                    btn[i, j].SetBounds(100 + (45 * i), 25 + (45 * j), 45, 45);
                    btn[i, j].BackColor = Color.PowderBlue;

                    // Give each button a random integer value of 1, 2 or 3
                    btn[i, j].Text = Convert.ToString(num = rnd.Next(1, 4));
                    btn[i, j].Name = Convert.ToString(i + "," + j);
                    btn[i, j].Click += new EventHandler(this.btnEvent_Click);
                    Controls.Add(btn[i, j]);
                }
            }

            // Gets display names for both players
            string value = "";
            if (InputBox("Choose your display name", "Enter your chosen display name", ref value, 50) == DialogResult.OK)
            {
                txtRed.Text = value;
            }
            value = "";
            if (InputBox("Choose your display name", "Enter your chosen display name", ref value, 50) == DialogResult.OK)
            {
                txtBlue.Text = value;
            }

            // Setting up the first turn [red always goes first]
            firstTurns = true;
            redPlaying = true;

            // Tell player who is first and what colour
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

        /*
         * When a button on the grid is clicked...
         * 
         *  - find its position on the grid
         *  - check if the game is over: if it is, edit the high scores accordingly and ask user if they want to play again then return function
         *  - if neither player is currently playing, if one colour's penguin has been clicked and the other colour played last, set the clicked colour to playing currently
         *  - if one colour is playing, check the current selection is a valid move and if it is, amend relevant values and play the sound effect
         */
        void btnEvent_Click(object sender, EventArgs e)
        {
            // Uses the name of the button to find its position
            string[] current = ((Button)sender).Name.Split(',');
            currentX = Int32.Parse(current[0]);
            currentY = Int32.Parse(current[1]);

            // Checks whether the current selection ends the game
            if (isGameOver(currentX, currentY))
            {
                // If first game then make high score file and save both scores and names
                if (redPoints >= bluePoints && !File.Exists(highScoresFile))
                {
                    File.AppendAllText(highScoresFile, txtRed.Text + ":" + redPoints + "\n" + txtBlue.Text + ":" + bluePoints + "\n");
                }
                else if (bluePoints >= redPoints && !File.Exists(highScoresFile))
                {
                    File.AppendAllText(highScoresFile, txtBlue.Text + ":" + bluePoints + "\n" + txtRed.Text + ":" + redPoints + "\n");
                }
                else
                {
                    // Gets the previous high scores from the file
                    string highScores = File.ReadAllText(highScoresFile);

                    // https://stackoverflow.com/questions/146204/duplicate-keys-in-net-dictionaries

                    // Splits up the previous high scores and saves them in an array
                    string[] scores = highScores.Split('\n');

                    // For separating the high score strings into the string names and integer point totals
                    List<KeyValuePair<int, string>> highScoreList = new List<KeyValuePair<int, string>>();

                    // Loops through the number of scores
                    for (int i = 0; i < scores.Length - 1; i++)
                    {
                        // Splits the strings into names and scores
                        string[] namesAndPoints = scores[i].Split(':');
                        int score = Int32.Parse(namesAndPoints[1]);
                        highScoreList.Add(new KeyValuePair<int, string>(score, namesAndPoints[0]));
                    }

                    highScores = "";

                    // Adds the current game's scores to the list of high scores
                    highScoreList.Add(new KeyValuePair<int, string>(redPoints, txtRed.Text));
                    highScoreList.Add(new KeyValuePair<int, string>(bluePoints, txtBlue.Text));

                    // welp this doesnt work. It can't sort this.
                    highScoreList.Sort(CompareNums);

                    // If the list is at its max length, drop the last two scores
                    if (highScoreList.Count == 7)
                    {
                        highScoreList.RemoveAt(6);
                        highScoreList.RemoveAt(5);
                    }
                    // If it is one off its max length, only drop the fifth score
                    else if (highScoreList.Count == 6)
                    {
                        highScoreList.RemoveAt(5);
                    }

                    // Sets up the scores for being written back to the file
                    foreach (KeyValuePair<int, string> kvp in highScoreList)
                    {
                        int keyScore = kvp.Key;
                        string valueName = kvp.Value;
                        highScores += valueName + ":" + keyScore + "\n";
                    }

                    // they are written back to front but that's it
                    File.WriteAllText(highScoresFile, highScores);
                }

                // Lets the user know the game is over and asks them if they want to play again
                gameOverBox();

                // Must return here as new tile must be selected to start game over if the user chooses to play again
                return;
            }
            // If neither player is currently moving
            if (redPlaying == false && bluePlaying == false)
            {
                // If the selected button is the red player and blue played last
                if ((((Button)sender).BackColor == Color.Red) && lastPlayed == 'b')
                {
                    // Red is currently playing
                    redPlaying = true;

                    // Change the current button to an empty square
                    ((Button)sender).BackColor = Color.Black;
                    ((Button)sender).BackgroundImage = null;
                    ((Button)sender).Text = "0";

                    // Plays the sound effect as long as the mute option is not enabled
                    if (muteSound == false)
                    {
                        splashSound.Play();
                    }

                    // Sets the current button as the previous tile
                    prevTile = ((Button)sender).Name;
                }
                // If the selected button is the blue player and red played last
                else if ((((Button)sender).BackColor == Color.Blue) && lastPlayed == 'r')
                {
                    // Blue is currently playing
                    bluePlaying = true;

                    // Change the current button to an empty square
                    ((Button)sender).BackColor = Color.Black;
                    ((Button)sender).BackgroundImage = null;
                    ((Button)sender).Text = "0";

                    // Plays the sound effect as long as the mute option is not enabled
                    if (muteSound == false)
                    {
                        splashSound.Play();
                    }

                    // Sets the current button as the previous tile
                    prevTile = ((Button)sender).Name;
                }
            }
            // If it is currently red's turn
            else if (redPlaying == true)
            {
                // Get the position of the previous tile
                string[] prev = prevTile.Split(',');
                prevX = Int32.Parse(prev[0]);
                prevY = Int32.Parse(prev[1]);

                // Get the position of the current tile
                current = ((Button)sender).Name.Split(',');
                currentX = Int32.Parse(current[0]);
                currentY = Int32.Parse(current[1]);

                // If the buttons are on the same y axis or x axis, or it is the first turn
                if ((currentX == prevX || currentY == prevY) || firstTurns == true)
                {
                    // If it is the x axis they share
                    if (currentX == prevX)
                    {
                        // Loop through all the tiles between them and make sure there are no obstructions
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

                    // If it is the y axis they share
                    if (currentY == prevY)
                    {
                        // Loop through all the tiles between them and make sure there are no obstructions
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

                    // If the current tile is available and there are no obstructions
                    if ((((Button)sender).BackColor == Color.PowderBlue) && obstruction == false)
                    {
                        // Change the current tile to the red penguin tile
                        ((Button)sender).BackColor = Color.Red;
                        ((Button)sender).BackgroundImage = fishGame.Properties.Resources.linux_penguin_red;
                        ((Button)sender).BackgroundImageLayout = ImageLayout.Center;

                        // Add the points collected from this move on the red player's total
                        num = Int32.Parse(((Button)sender).Text);
                        redPoints += num;
                        ((Label)lblRedPoints).Text = redPoints.ToString();

                        // Red is done playing and is now the previous player
                        redPlaying = false;
                        lastPlayed = 'r';

                        // If it is the first turns, make blue now playing
                        if (firstTurns == true)
                            bluePlaying = true;
                    }
                }
            }
            // If it is currently blue's turn
            else if (bluePlaying == true)
            {
                // Get the position of the previous tile
                string[] prev = prevTile.Split(',');
                prevX = Int32.Parse(prev[0]);
                prevY = Int32.Parse(prev[1]);

                // Get the position of the current tile
                current = ((Button)sender).Name.Split(',');
                currentX = Int32.Parse(current[0]);
                currentY = Int32.Parse(current[1]);

                // If the buttons are on the same y axis or x axis, or it is the first turn
                if ((currentX == prevX || currentY == prevY) || firstTurns == true)
                {
                    // If it is the x axis they share
                    if (currentX == prevX)
                    {
                        // Loop through all the tiles between them and make sure there are no obstructions
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

                    // If it is the y axis they share
                    if (currentY == prevY)
                    {
                        // Loop through all the tiles between them and make sure there are no obstructions
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

                    // If the current tile is available and there are no obstructions
                    if ((((Button)sender).BackColor == Color.PowderBlue) && obstruction == false)
                    {
                        // Change the current tile to the blue penguin tile
                        ((Button)sender).BackColor = Color.Blue;
                        ((Button)sender).BackgroundImage = fishGame.Properties.Resources.linux_penguin_blue;
                        ((Button)sender).BackgroundImageLayout = ImageLayout.Center;

                        // Add the points collected from this move on the blue player's total
                        num = Int32.Parse(((Button)sender).Text);
                        bluePoints += num;
                        ((Label)lblBluePoints).Text = bluePoints.ToString();

                        // Blue is done playing and is now the previous player
                        bluePlaying = false;
                        lastPlayed = 'b';

                        // If it is the first turns, it is no longer the first turns
                        if (firstTurns == true)
                            firstTurns = false;
                    }
                }
            }
            // Reset the obstruction flag to false
            obstruction = false;
        }

        // ideally change this more?
        private int CompareNums(KeyValuePair<int, string> compareThis, KeyValuePair<int, string> withThis)
        {
            return withThis.Key.CompareTo(compareThis.Key);
        }

        /*
         * Checks whether the game is concluded, i.e. at least one penguin cannot make a valid move.
         */
        private bool isGameOver(int x, int y)
        {
            bool gameOver = true;
            // can move left
            if (x - 1 >= 0 && btn[x - 1, y].BackColor == Color.PowderBlue)
                return gameOver = false;
            //can move right
            if (x + 1 <= 9 && btn[x + 1, y].BackColor == Color.PowderBlue)
                return gameOver = false;
            // can move up
            if (y - 1 >= 0 && btn[x, y - 1].BackColor == Color.PowderBlue)
                return gameOver = false;
            // can move down
            if (y + 1 <= 9 && btn[x, y + 1].BackColor == Color.PowderBlue)
                return gameOver = false;
            return gameOver;
        }

        /*
         * Lets the user know that the game is finished and offers the 'Play again?' feature.
         */
        private void gameOverBox()
        {
            DialogResult result;

            // Checks which player won the game and displays the appropriate message box to the user and asks whether the user would like to play again
            if (redPoints > bluePoints)
            {
                result = MessageBox.Show("Game Complete! Winner is " + txtRed.Text + " with " + redPoints + " points. Would you like to play again?", "Game Complete", MessageBoxButtons.YesNo);
            }
            else
            {
                result = MessageBox.Show("Game Complete! Winner is " + txtBlue.Text + " with " + bluePoints + " points. Would you like to play again?", "Game Complete", MessageBoxButtons.YesNo);
            }

            // If the player selects yes, they would like to play again, reset the game
            if (result == DialogResult.Yes)
            {
                ResetGame();
            }

            // Otherwise close the application
            else
            {
                Close();
            }
        }

        /*
         * Allows the user to close the game via the options menu.
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /*
        * Lets the user know about the creators of the game.
        */
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

        /*
         * Allows the user to mute and unmute the sound effects in the game.
         */
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

        /*
         * Allows the user to see the previous top 5 high scores via the options menu.
         */
        private void highScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Checks whether the file exists, if it does, display the high scores, if not, display an appropriate message
            if (File.Exists(highScoresFile))
            {
                string highScoresMenu = File.ReadAllText(highScoresFile);
                DialogResult result;
                result = MessageBox.Show(highScoresMenu, "High Scores");
            }
            else
            {
                DialogResult result;
                result = MessageBox.Show("No high scores yet.\n Play a game first :)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
         * Resets all relevant values so the game can start again.
         */
        private void ResetGame()
        {
            // Reset variables
            redPoints = 0;
            bluePoints = 0;
            redPlaying = true;
            bluePlaying = false;
            firstTurns = true;
            prevTile = "-1,-1";
            prevX = -1;
            prevY = -1;
            currentX = -1;
            currentY = -1;
            obstruction = false;
            lastPlayed = '\0';

            // Clear the board
            for (int i = 0; i < btn.GetLength(0); i++)
            {
                for (int j = 0; j < btn.GetLength(1); j++)
                {
                    btn[i, j].BackColor = Color.PowderBlue;
                    btn[i, j].Text = Convert.ToString(num = rnd.Next(1, 4));
                    btn[i, j].Name = Convert.ToString(i + "," + j);
                    btn[i, j].BackgroundImage = null;
                }
            }

            // Reset points labels
            lblRedPoints.Text = "0";
            lblBluePoints.Text = "0";

            // Start game message and go
            DialogResult result;
            result = MessageBox.Show(txtRed.Text + " plays first! You are red. Click a box to make your first move. \n" + txtBlue.Text + " will play second. You are blue.", "First Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("*Oi! That's My Sea Creature*\nBased on the boardgame 'Hey that's my fish!'.\n\nThe ice is breaking up! Grab all the fish you can before they slip away. If you don’t, another penguin will. It’s every penguin for itself. Your penguin must race across the rapidly dwindling ice floe to collect the juiciest fish and block off their rivals. But your penguin better stay alert! If a penguin gets stuck on an ice floe, he’s done. Seemingly simple, your goal will be thwarted by devious penguins and an ever-shrinking game board. What strategy will you construct to bypass the competition?\n\nThe player who has collected the most fish by the end of the game wins.\n\nMove your penguin! During this step, the player moves their penguin as far as they want in a straight line. The penguin may move in any one of the four directions of the square, but it cannot change direction during the move. The penguin can only move onto unoccupied ice floes. It cannot move onto or through floes occupied by another penguin or spaces without ice floes", "Rules", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}


