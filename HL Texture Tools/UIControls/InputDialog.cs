using System.Windows.Forms;

namespace HLTextureTools.UIControls
{
    public partial class InputDialog : Form
    {
        public string ReturnValue { get; set; }

        public InputDialog(string title, string promptText, string value, int maxLen = -1)
        {
            InitializeComponent();

            Text = title;
            label1.Text = promptText;
            textBox1.Text = value;

            if (maxLen != -1)
            {
                textBox1.MaxLength = maxLen;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            ReturnValue = textBox1.Text;
        }
    }
}
