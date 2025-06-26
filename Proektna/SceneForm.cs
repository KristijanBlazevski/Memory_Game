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
        
        
        public SceneForm(string selectiranaVrednost)
        {
            InitializeComponent();
            tlpContainer.RowStyles.Clear();
            tlpContainer.ColumnStyles.Clear();
            switch (selectiranaVrednost)
            {
                case "2":
                    
                    tlpContainer.RowCount = 2;
                    tlpContainer.ColumnCount = 2;

                    break;
                case "4":
                    tlpContainer.RowCount = 4;
                    tlpContainer.ColumnCount = 4;
                    break;
                case "6":
                    tlpContainer.RowCount = 6;
                    tlpContainer.ColumnCount = 6;
                    break;
                case "8":
                    tlpContainer.RowCount = 8;
                    tlpContainer.ColumnCount = 8;
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

        }
    }
}
