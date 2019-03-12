using DigitalSignage.BLL;
using DigitalSignage.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalSignage.UI.Campaign_Forms
{
    public partial class CampaignManageForm : Form
    {
        private CampaignService iCampaignService;

        public CampaignManageForm()
        {
            this.iCampaignService = new CampaignService();
            InitializeComponent();
            campaignsGridView.AutoGenerateColumns = false;
            campaignsGridView.DataSource = this.iCampaignService.GetAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CampaignForm_Load(object sender, EventArgs e)
        {

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            new CampaignEditForm(Convert.ToInt32(this.campaignsGridView.CurrentRow.Cells["Id"].Value.ToString())).ShowDialog();
        }

        private void campaignsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void createButton_Click(object sender, EventArgs e)
        {
            new CampaignEditForm(-1).ShowDialog();
        }
    }
}
