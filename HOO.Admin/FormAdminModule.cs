using System;
using System.Windows.Forms;

namespace HOO.Admin
{
    public partial class FormAdminModule : Form
    {
        public FormAdminModule()
        {
            InitializeComponent();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
