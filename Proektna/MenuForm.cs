namespace Proektna
{
    public partial class MenuForm : Form
    {

        public string? selektiranaVrednost { get; set; }
        public MenuForm()
        {
            InitializeComponent();

        }
        /// <summary>
        /// Button is disabled until both player names are entered and a game mode is selected.
        /// </summary>
        private void UpdateStartButtonState()
        {
            if (!string.IsNullOrWhiteSpace(playerOneName.Text) &&
                !string.IsNullOrWhiteSpace(playerTwoName.Text) &&
                comboBox1.SelectedItem != null)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selektiranaVrednost = comboBox1.SelectedItem?.ToString();
            SceneForm scene = new SceneForm(selektiranaVrednost, playerOneName.Text, playerTwoName.Text);

            scene.Show();
        }

        public string? GetSelectedValue()
        {
            return selektiranaVrednost;
        }

        private void rules_Click(object sender, EventArgs e)
        {
            Rules rules = new Rules();
            rules.Show();
        }

        private void playerOneName_TextChanged(object sender, EventArgs e)
        {
            UpdateStartButtonState();
        }

        private void playerTwoName_TextChanged(object sender, EventArgs e)
        {
            UpdateStartButtonState();
        }
    }
}