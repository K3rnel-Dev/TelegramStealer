using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TgBuilder.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void Lnklabel_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/k3rnel-dev");
        }

        private void LnkLabelPrototype_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/k3rnel-dev");
        }
    }
}
