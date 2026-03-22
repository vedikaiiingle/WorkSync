using SE_Project.Helpers;
using System;
using System.Windows.Forms;

namespace SE_Project.Pages
{
    public partial class Starred : UserControl
    {
        DBHelper db = new DBHelper();

        // Manual Definition (Errors hatane ke liye)
        private FlowLayoutPanel flowLayoutPanelStarred;

        public Starred()
        {
            InitializeComponent();

            // Emergency Manual Creation
            flowLayoutPanelStarred = new FlowLayoutPanel();
            flowLayoutPanelStarred.Name = "flowLayoutPanelStarred";
            flowLayoutPanelStarred.Dock = DockStyle.Fill;
            flowLayoutPanelStarred.AutoScroll = true;
            this.Controls.Add(flowLayoutPanelStarred);
            flowLayoutPanelStarred.BringToFront();
        }

        private void Starred_Load(object sender, EventArgs e)
        {
            try
            {
                flowLayoutPanelStarred.Controls.Clear();
                db.getButtons("SELECT * FROM projects WHERE is_starred = 1", flowLayoutPanelStarred);
            }
            catch (Exception ex)
            {
            }
        }
    }
}