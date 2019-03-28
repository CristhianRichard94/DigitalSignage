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
    public partial class RSSEditForm : Form
    {
        private RSSSourceDTO iRSSSource;

        public RSSEditForm(RSSSourceDTO pRSSSource)
        {
            InitializeComponent();

            if (pRSSSource == null)
            {
                this.RSSSource = new RSSSourceDTO();
            }
            else
            {
                this.RSSSource = pRSSSource;
                this.textBox1.Text = this.RSSSource.Url;
                this.textBox2.Text = this.RSSSource.Description;
            }
        }

        public RSSSourceDTO RSSSource { get => iRSSSource; set => iRSSSource = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            var confirmResult = new NotificationForm(MessageBoxButtons.YesNo, "¿Está seguro que desea cancelar las operaciones realizadas? se perderan los cambios",
                                     "Cancelar");
            confirmResult.ShowDialog();
            if (confirmResult.DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.RSSSource.Url = this.textBox1.Text;
            this.RSSSource.Description = this.textBox2.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
