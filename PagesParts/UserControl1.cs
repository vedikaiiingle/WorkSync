using System;
using System.Windows.Forms;
using SE_Project.Helpers;

namespace SE_Project.PagesParts
{
    public partial class UserControl1 : UserControl
    {
        DBHelper db = new DBHelper();

        public string ProjectTitle { get; set; }
        public string ProjectDesc { get; set; }

        public UserControl1()
        {
            InitializeComponent();
        }

        // --- IN KHHALI METHODS KO REHNE DO, DESIGNER INHE DHOOND RAHA HAI ---

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Isse khali rakho
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {
            // Isse khali rakho
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Isse khali rakho
        }

        // --- AGAR KOI MOVE BUTTON HAI TOH USKA LOGIC ---
        private void btnMove_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProjectTitle))
            {
                db.MoveProject(this.ProjectTitle, this.ProjectDesc, "projects", "completed");
                this.Parent.Controls.Remove(this);
            }
        }
    }
}