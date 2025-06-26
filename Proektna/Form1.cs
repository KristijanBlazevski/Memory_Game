namespace Proektna
{
    public partial class Form1 : Form
    {

        public string selektiranaVrednost { get; set; }
        public Form1()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            selektiranaVrednost = comboBox1.SelectedItem.ToString();
            SceneForm scene = new SceneForm(selektiranaVrednost);
            
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
    }
}