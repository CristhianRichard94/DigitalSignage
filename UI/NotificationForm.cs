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
    public partial class NotificationForm : Form
    {
        public NotificationForm(MessageBoxButtons pMessageBoxButtons, string pText, string pTitle)
        {
            InitializeComponent();

            this.titleLabel.Text = pTitle;
            this.infoLabel.Text = pText;

            if (pMessageBoxButtons == MessageBoxButtons.YesNo)
            {
                this.acceptButton.Visible = true;
                this.cancelButton.Visible = true;
                this.okButton.Visible = false;
            } else
            {
                this.acceptButton.Visible = false;
                this.cancelButton.Visible = false;
                this.okButton.Visible = true;
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
