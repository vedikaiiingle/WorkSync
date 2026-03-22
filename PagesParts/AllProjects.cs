using SE_Project.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Project.PagesParts
{
    public partial class AllProjects : UserControl
    {
        DBHelper db;

        public AllProjects()
        {
            InitializeComponent();
            db = new DBHelper();
        }

        // Ye properties cards identify karne ke liye hain
        public string ProjectDesc { get; internal set; }
        public string ProjectTitle { get; internal set; }

        // --- MAIN LOAD EVENT ---
        private void AllProjects_Load(object sender, EventArgs e)
        {
            // Pehle panel clear karo phir cards load karo
            // Note: 'AllProjectsPanel' aapke designer mein FlowLayoutPanel ka naam hona chahiye
            if (AllProjectsPanel != null)
            {
                AllProjectsPanel.Controls.Clear();
                db.getButtons("SELECT * FROM projects", AllProjectsPanel);
            }
        }

        // --- KHHALI METHODS (DESIGNER ERRORS SE BACHNE KE LIYE) ---
        private void PanelProjectsAll_Paint(object sender, PaintEventArgs e) { }
        private void projectCard1_Load(object sender, EventArgs e) { }
        private void projectCard2_Load(object sender, EventArgs e) { }
        private void projectCard3_Load(object sender, EventArgs e) { }
        private void projectCard4_Load(object sender, EventArgs e) { }
        private void projectCard5_Load(object sender, EventArgs e) { }
        private void projectCard6_Load(object sender, EventArgs e) { }
    }
}