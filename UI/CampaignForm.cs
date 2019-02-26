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

namespace DigitalSignage.UI
{
    public partial class CampaignForm : Form
    {
        private IEnumerator<Campaign> cp;

        public CampaignForm(IEnumerator<Campaign> campaigns)
        {
            cp = campaigns;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            cp.MoveNext();
            try
            {
                label1.Text += "\n" + cp.Current.Description;
            }
            catch (Exception exception)
            {
                label1.Text += "\n No mas campañas" + exception.Message;
                this.Close();
            }
        }

        private void CampaignForm_Load(object sender, EventArgs e)
        {

        }
    }
}
