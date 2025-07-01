namespace Proektna
{
    public partial class MenuForm : Form
    {

        public string selektiranaVrednost { get; set; }
        public MenuForm()
        {
            InitializeComponent();
           
        }


        private void button1_Click(object sender, EventArgs e)
        {
            selektiranaVrednost = comboBox1.SelectedItem.ToString();
            SceneForm scene = new SceneForm(selektiranaVrednost,playerOneName.Text,playerTwoName.Text);
            if (string.IsNullOrEmpty(playerOneName.Text))
            {
                MessageBox.Show("Please enter a name for Player 1");
            }
            else if (string.IsNullOrEmpty(playerTwoName.Text))
            {
                MessageBox.Show("Please enter a name for Player 2!");
            }
            else
                scene.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                button1.Enabled = true;
            }
            
        }

        public string GetSelectedValue()
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
            Player player1 = new Player(playerOneName.Text);
                              

        }

        private void playerTwoName_TextChanged(object sender, EventArgs e)
        {
            Player player2 = new Player(playerTwoName.Text);
                        
        }
    }
}