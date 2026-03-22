using SE_Project.Helpers;
using System;
using System.Windows.Forms;

namespace SE_Project.Pages
{
    public partial class DashBoard : UserControl
    {
        DBHelper db;

        public DashBoard()
        {
            InitializeComponent();
            db = new DBHelper();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            RefreshDashboardCounts();
        }

        public void RefreshDashboardCounts()
        {
            try
            {


                // 1. Total Projects
                int totalProjects = db.GetTotalRowCount("projects");
                
                if (this.Controls.ContainsKey("lblTotalProjects"))
                    ((Label)this.Controls["lblTotalProjects"]).Text = totalProjects.ToString();

                // 2. Completed Projects
                int completedCount = db.GetTotalRowCount("completed");
                if (this.Controls.ContainsKey("lblCompleted"))
                    ((Label)this.Controls["lblCompleted"]).Text = completedCount.ToString();

                // 3. In Progress Projects
                int inProgressCount = db.GetTotalRowCount("inprogress");
                if (this.Controls.ContainsKey("lblInProgress"))
                    ((Label)this.Controls["lblInProgress"]).Text = inProgressCount.ToString();

                // 4. Starred
                int starredCount = db.GetTotalRowCount("starred");
                if (this.Controls.ContainsKey("lblStarred"))
                    ((Label)this.Controls["lblStarred"]).Text = starredCount.ToString();

                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}