using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proektna
{
    public partial class SceneForm : Form
    {
        Random random = new Random();

        List<string> Icons;

        Label firstClicked, secondClicked;

        Player player1, player2;
        Player currentPlayer;

        bool isChecking = false;
        int pairsFound = 0;
        int totalPairs = 0;

        public SceneForm(string selectiranaVrednost)
        {
            InitializeComponent();
            player1 = new Player("Player 1");
            player2 = new Player("Player 2");
            currentPlayer = player1;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.BackColor = Color.Red;
            resetFirst.Enabled = true;
            jokerFirst.Enabled = true;
            resetSecond.Enabled = false;
            jokerSecond.Enabled = false;

            timer1.Interval = 1000;
            JokerTick.Interval = 600;
            Icons = GenerateIcons(selectiranaVrednost);
            totalPairs = int.Parse(selectiranaVrednost);



            switch (selectiranaVrednost)
            {
                case "4":
                    tlpContainer.RowCount = 2;
                    tlpContainer.ColumnCount = 4;
                    player1.timeLeft = 30;
                    player2.timeLeft = 30;
                    break;
                case "6":
                    tlpContainer.RowCount = 3;
                    tlpContainer.ColumnCount = 4;
                    player1.timeLeft = 40;
                    player2.timeLeft = 40;
                    break;
                case "8":
                    tlpContainer.RowCount = 4;
                    tlpContainer.ColumnCount = 4;
                    player1.timeLeft = 50;
                    player2.timeLeft = 50;
                    break;
                case "10":
                    tlpContainer.RowCount = 5;
                    tlpContainer.ColumnCount = 4;
                    player1.timeLeft = 60;
                    player2.timeLeft = 60;
                    break;
                default:
                    tlpContainer.RowCount = 3;
                    tlpContainer.ColumnCount = 3;
                    break;
            }

            for (int i = 0; i < tlpContainer.RowCount; i++)
            {
                tlpContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / tlpContainer.RowCount));
            }
            for (int i = 0; i < tlpContainer.ColumnCount; i++)
            {
                tlpContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / tlpContainer.ColumnCount));
            }
            GenerateLabels(selectiranaVrednost);
            lTimerFirst.Text = "Time Left: " + player1.timeLeft;
            lTimerSecond.Text = "Time Left: " + player2.timeLeft;
            TimeLeftTick.Interval = 1000;
            TimeLeftTick.Start();
        }
        private void UpdatePlayerUI()
        {
            pointsFirst.Text = "Points: " + player1.score;
            pointsSecond.Text = "Points: " + player2.score;
        }

        private void SwitchPlayer()
        {
            if (currentPlayer == player1)
            {
                currentPlayer = player2;
                resetFirst.Enabled = false;
                jokerFirst.Enabled = false;
                resetSecond.Enabled = true;
                jokerSecond.Enabled = true;

                panel2.BorderStyle = BorderStyle.FixedSingle;
                panel2.BackColor = Color.Yellow;
                panel1.BorderStyle = BorderStyle.None;
                panel1.ForeColor = Color.Black;
                panel1.BackColor = Color.SkyBlue;

                if (player2.UsedJoker == true)
                {
                    jokerSecond.Enabled = false;
                }
                if (player2.UsedReset == true)
                {
                    resetSecond.Enabled = false;
                }
            }
            else
            {
                currentPlayer = player1;
                resetFirst.Enabled = true;
                jokerFirst.Enabled = true;
                resetSecond.Enabled = false;
                jokerSecond.Enabled = false;

                panel1.BorderStyle = BorderStyle.FixedSingle;
                panel1.BackColor = Color.Red;
                panel2.BorderStyle = BorderStyle.None;
                panel2.ForeColor = Color.Black;
                panel2.BackColor = Color.SkyBlue;

                if (player1.UsedJoker == true)
                {
                    jokerFirst.Enabled = false;
                }
                if (player1.UsedReset == true)
                {
                    resetFirst.Enabled = false;
                }

            }

        }

        private void GenerateLabels(string selectiranaVrednost)
        {
            int totalIcons = int.Parse(selectiranaVrednost);

            int widthCell = tlpContainer.ClientSize.Width / tlpContainer.ColumnCount;
            int heightCell = tlpContainer.ClientSize.Height / tlpContainer.RowCount;

            for (int i = 0; i < tlpContainer.ColumnCount; i++)
            {
                for (int j = 0; j < tlpContainer.RowCount; j++)
                {
                    int randomNumber = random.Next(0, Icons.Count);
                    Label label = new Label
                    {
                        Text = Icons[randomNumber],
                        TextAlign = ContentAlignment.MiddleCenter,
                        Width = widthCell,
                        Height = heightCell,
                        Font = new Font("Webdings", 72, FontStyle.Bold),
                        ForeColor = Color.SteelBlue

                    };

                    tlpContainer.Controls.Add(label, i, j);
                    Icons.RemoveAt(randomNumber);

                    label_Click(label);

                }

            }

        }
        public void label_Click(Label label)
        {
            label.Click += (sender, e) =>
            {
                if (isChecking)
                    return;
                Label clickedLabel = sender as Label;
                if (clickedLabel == null || clickedLabel.ForeColor == Color.Black)
                    return;

                clickedLabel.ForeColor = Color.Black;
                clickedLabel.BackColor = Color.Red;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    return;
                }

                secondClicked = clickedLabel;

                if (firstClicked.Text != secondClicked.Text)
                {
                    currentPlayer.score -= 1;
                    UpdatePlayerUI();
                    SwitchPlayer();

                }

                if (firstClicked.Text == secondClicked.Text)
                {
                    pairsFound++;

                    currentPlayer.score += 3;

                    firstClicked = null;
                    secondClicked = null;
                    UpdatePlayerUI();

                    if (pairsFound == totalPairs)
                    {
                        CheckWinner();

                    }
                    return;
                }
                else

                    isChecking = true;
                timer1.Start();

            };
        }

        private List<string> GenerateIcons(string selectiranaVrednost)
        {
            int totalIcons = int.Parse(selectiranaVrednost);
            List<string> icons = new List<string>();

            string possibleIcons = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!,";

            for (int i = 0; i < totalIcons; i++)
            {
                int randomIndex = random.Next(possibleIcons.Length);
                char icon = possibleIcons[randomIndex];
                icons.Add(icon.ToString());
                icons.Add(icon.ToString());
                possibleIcons = possibleIcons.Remove(randomIndex, 1);
            }

            return icons;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (firstClicked == null || secondClicked == null)
            {
                timer1.Stop();
                isChecking = false;
                return;
            }
            timer1.Stop();
            firstClicked.ForeColor = Color.SteelBlue;
            firstClicked.BackColor = Color.SteelBlue;
            secondClicked.ForeColor = Color.SteelBlue;
            secondClicked.BackColor = Color.SteelBlue;
            firstClicked = null;
            secondClicked = null;
            isChecking = false;
            UpdatePlayerUI();
        }

        private void jokerFirst_Click(object sender, EventArgs e)
        {
            player1.UsedJoker = true;
            player1.score -= 3;
            jokerFirst.Enabled = false;

            foreach (Label label in tlpContainer.Controls)
            {

                if (label.ForeColor == Color.Black)
                {
                    label.ForeColor = Color.Black;
                    continue;
                }
                else
                    label.ForeColor = Color.Gray;
            }

            JokerTick.Start();
            UpdatePlayerUI();

        }

        private void jokerSecond_Click(object sender, EventArgs e)
        {
            player2.UsedJoker = true;
            player2.score -= 3;
            jokerSecond.Enabled = false;

            foreach (Label label in tlpContainer.Controls)
            {

                if (label.ForeColor == Color.Black)
                {
                    label.ForeColor = Color.Black;
                    continue;
                }
                else
                    label.ForeColor = Color.Gray;
            }

            JokerTick.Start();
            UpdatePlayerUI();
        }

        private void JokerTick_Tick(object sender, EventArgs e)
        {

            foreach (Label label in tlpContainer.Controls)
            {

                if (label.ForeColor == Color.Black)
                {
                    label.ForeColor = Color.Black;
                    continue;
                }
                else
                    label.ForeColor = Color.SteelBlue;


            }
            JokerTick.Stop();

        }

        private void resetFirst_Click(object sender, EventArgs e)
        {
            resetFirst.Enabled = false;
            player1.UsedReset = true;
            foreach (Label label in tlpContainer.Controls)
            {
                label.ForeColor = Color.SteelBlue;
                label.BackColor = Color.SteelBlue;
            }
        }

        private void resetSecond_Click(object sender, EventArgs e)
        {
            resetSecond.Enabled = false;
            player2.UsedReset = true;
            foreach (Label label in tlpContainer.Controls)
            {
                label.ForeColor = Color.SteelBlue;
                label.BackColor = Color.SteelBlue;
            }
        }

        private Player CheckWinner()
        {
            if (pairsFound == totalPairs)
            {

                if (player1.score == player2.score)
                {
                    TimeLeftTick.Stop();
                    MessageBox.Show("It's a tie!");
                }
                else
                {
                    return player1.score > player2.score ? player1 : player2;
                }
            }

            if (player1.timeLeft <= 0)
            {
                return player2;
            }

            if (player2.timeLeft <= 0)
            {
                return player1;
            }

            return null;
        }

        private void TimeLeftTick_Tick(object sender, EventArgs e)
        {
            if (currentPlayer == player1)
            {
                player1.timeLeft--;
                lTimerFirst.Text = "Time Left: " + player1.timeLeft;

            }
            else
            {
                player2.timeLeft--;
                lTimerSecond.Text = "Time Left: " + player2.timeLeft;

            }

            Player winner = CheckWinner();
            if (winner != null)
            {
                TimeLeftTick.Stop();
                if (winner == player1 && player2.timeLeft <= 0)
                {
                    MessageBox.Show($"Time is up for Player 2!\nWinner: {winner.Name}\nScore: {winner.score}");
                }
                else if (winner == player2 && player1.timeLeft <= 0)
                {
                    MessageBox.Show($"Time is up for Player 1!\nWinner: {winner.Name}\nScore: {winner.score}");
                }
                else
                    MessageBox.Show($"Winner: {winner.Name}\nScore: {winner.score}");

                this.Close();
            }
        }
    }
}