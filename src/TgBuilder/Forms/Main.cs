using System.Configuration;
using System.Windows.Forms;
using TgBuilder.Core;
using TgBuilder.Forms;

namespace TgBuilder
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void BuildBtn_Click(object sender, System.EventArgs e)
        {
            string
                Token = TokenBox.Text,
                Chatid = ChatidBox.Text;

            bool
                UseObfuscate = ObfuscatorChk.Checked,
                UseMelting = MeltFile_Chk.Checked;

            if (string.IsNullOrEmpty(Token) || string.IsNullOrEmpty(Chatid))
            {
                MessageBox.Show("Fields cannot be empty!", "- Information", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            using (SaveFileDialog save = new SaveFileDialog())
            {
                save.Title = "Select compiled file:";
                save.Filter = "EXE Files (*.exe)|*.exe";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    
                    string result = Compilator.Compilate(Token, Chatid, save.FileName, UseObfuscate, UseMelting);
                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show($"Result Compilation: {result}");
                    }
                }
            } 
        }

        private void AboutBtn_Click(object sender, System.EventArgs e)
        {
            About form = new About();
            form.ShowDialog();
        }
    }
}
