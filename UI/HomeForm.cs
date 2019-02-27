using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalSignage.UI.Banner_Forms;
using DigitalSignage.UI.Campaign_Forms;
using DigitalSignage.UI.Properties;
using DigitalSignage.UI.Operative_Form;
using DigitalSignage.UI.RSS_Forms;

namespace DigitalSignage.UI
{
    public partial class HomeForm : Form
    {
        private bool isCollapsed = true;

        public HomeForm()
        {
            InitializeComponent();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }


        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                manageOptions.Image = Resources.dropdownarrow2;
                dropdownPanel.Height += 10;
                if (dropdownPanel.Size == dropdownPanel.MaximumSize)
                {
                    timer1.Stop();
                    this.isCollapsed = false;
                }
            }
            else
            {
                manageOptions.Image = Resources.dropdownarrow1;
                dropdownPanel.Height -= 10;
                if (dropdownPanel.Size == dropdownPanel.MinimumSize)
                {
                    timer1.Stop();
                    this.isCollapsed = true;
                }
            }
        }

        private void manageOptions_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void operativeScreen_Click(object sender, EventArgs e)
        {
            OperativeForm operativeForm = new OperativeForm();
            operativeForm.ShowDialog();
        }
        private void campManage_Click(object sender, EventArgs e)
        {
            CampaignManageForm campaignManageForm = new CampaignManageForm();
            campaignManageForm.ShowDialog();
        }

        private void bannersManage_Click(object sender, EventArgs e)
        {
            BannerManageForm bannerManageForm = new BannerManageForm();
            bannerManageForm.ShowDialog();
        }

        private void rssManage_Click(object sender, EventArgs e)
        {
            RSSManageForm rSSManageForm= new RSSManageForm();
            rSSManageForm.ShowDialog();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
