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

        private void tsNewWorld_Click(object sender, EventArgs e)
        {
            FormUniverseGeneration fug = new FormUniverseGeneration();
            fug.MdiParent = this;
            fug.Show();
        }
    }
}
