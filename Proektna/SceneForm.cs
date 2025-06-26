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

namespace Proektna
{
    public partial class SceneForm : Form
    {
        Random random = new Random();

        List<string> Icons;

        public SceneForm(string selectiranaVrednost)
        {
            InitializeComponent();
            tlpContainer.RowStyles.Clear();
            tlpContainer.ColumnStyles.Clear();
            Icons = GenerateIcons(selectiranaVrednost);
            switch (selectiranaVrednost)
            {
                case "2":

                    tlpContainer.RowCount = 2;
                    tlpContainer.ColumnCount = 2;

                    break;
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

                    };

                    tlpContainer.Controls.Add(label, i, j);
                    Icons.RemoveAt(randomNumber);
                }

            }
        }

        private List<string> GenerateIcons(string selectiranaVrednost)
        {
            int totalIcons = int.Parse(selectiranaVrednost);
            List<string> icons = new List<string>();

            string possibleIcons = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i < totalIcons; i++)
            {
                int randomIndex = random.Next(possibleIcons.Length);
                char icon = possibleIcons[randomIndex];
                icons.Add(icon.ToString());
                icons.Add(icon.ToString()); // Add the same icon twice for matching
                possibleIcons = possibleIcons.Remove(randomIndex, 1); // Remove used icon to avoid duplicates
            }

            return icons;
        }
    }
}