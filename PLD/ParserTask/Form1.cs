using com.calitha.goldparser;

namespace ParserTask
{
    public partial class Form1 : Form
    {
        MyParser p;

        public Form1()
        {
            InitializeComponent();
            p = new MyParser("Task.cgt", listBox1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            p.Parse(textBox1.Text);

        }
    }
}