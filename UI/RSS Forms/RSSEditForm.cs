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
            }
        }

        public RSSSourceDTO RSSSource { get => iRSSSource; set => iRSSSource = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
