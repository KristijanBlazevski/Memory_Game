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
           
            timer1.Interval = 1000;            
            Icons = GenerateIcons(selectiranaVrednost);
            totalPairs = int.Parse(selectiranaVrednost);
            labelPlayerTurn.Text = currentPlayer.Name;
            switch (selectiranaVrednost)
            {
                case "4":
                    tlpContainer.RowCount = 2;
                    tlpContainer.ColumnCount = 4;
                    break;
                case "6":
                    tlpContainer.RowCount = 3;
                    tlpContainer.ColumnCount = 4;
                    break;
                case "8":
                    tlpContainer.RowCount = 4;
                    tlpContainer.ColumnCount = 4;
                    break;
                case "10":
                    tlpContainer.RowCount = 5;
                    tlpContainer.ColumnCount = 4;
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
        }

        private void UpdatePlayerUI()
        {
            labelPlayerTurn.Text = currentPlayer.Name;
            pointsFirst.Text = "Points: " + player1.score;
            pointsSecond.Text = "Points: " + player2.score;
        }

        private void SwitchPlayer()
        {
            if(currentPlayer == player1)
            {
                currentPlayer = player2;
            }
            else
            {
                currentPlayer = player1;
            }
        }

        private void EndGame()
        {
            string winner;
            int winnerScore;
            if (player1.score > player2.score)
            {
                winner = player1.Name;
                winnerScore = player1.score;
                MessageBox.Show("Game Over! Winner: " + winner + "\n Score: " + winnerScore);
            }
            else if (player2.score > player1.score)
            {
                winner = player2.Name;
                winnerScore = player2.score;
                MessageBox.Show("Game Over! Winner: " + winner + "\n Score: " + winnerScore);
            }
            else
            {
               MessageBox.Show("It's a tie!");
               
            }
                        
            this.Close();
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
                        EndGame();
                        
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
                
    }
}