using DigitalSignage.BLL;
using DigitalSignage.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalSignage.UI.RSS_Forms
{
    public partial class RSSManageForm : Form
    {

        private RSSSourceService iRSSSourceService;
        private IEnumerable<RSSSourceDTO> iRSSSources;

        public IEnumerable<RSSSourceDTO> RSSSources { get => iRSSSources; set => iRSSSources = value; }

        public RSSManageForm()
        {
            InitializeComponent();
            this.iRSSSourceService = new RSSSourceService();
            this.RSSSources = this.iRSSSourceService.GetAll();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = this.RSSSources;
        }

        private void RSSManageForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RSSEditForm rSSEditForm = new RSSEditForm(null);
            rSSEditForm.ShowDialog();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            RSSEditForm rSSEditForm = new RSSEditForm((RSSSourceDTO)this.dataGridView1.SelectedRows[0].DataBoundItem);
            rSSEditForm.ShowDialog();
        }
    }
}
