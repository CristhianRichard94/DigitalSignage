using DigitalSignage.BLL.RSSReader;
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
        private IRSSReader iRSSReader;

        public RSSEditForm(IRSSReader rSSReader,RSSSourceDTO pRSSSource)
        {
            InitializeComponent();

            this.RSSReader = rSSReader;

            if (pRSSSource == null)
            {
                this.RSSSource = new RSSSourceDTO();
                this.RSSSource.RSSItems = new List<RSSItemDTO>();
            }
            else
            {
                this.RSSSource = pRSSSource;
                this.loadSource();
            }
        }

        public RSSSourceDTO RSSSource { get => IRSSSource; set => IRSSSource = value; }
        public RSSSourceDTO IRSSSource { get => iRSSSource; set => iRSSSource = value; }
        public IRSSReader RSSReader { get => iRSSReader; set => iRSSReader = value; }

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
            try
            {
                this.saveSource();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception exc)
            {
                new NotificationForm(MessageBoxButtons.OK, exc.Message, "Error").ShowDialog();
            }

        }


        void loadSource()
        {
            this.textBox1.Text = this.RSSSource.Url;
            this.textBox2.Text = this.RSSSource.Description;
            this.rSSItemsGridView.DataSource = this.RSSSource.RSSItems;
        }

        void saveSource()
        {
            this.RSSSource.Url = this.textBox1.Text;
            this.RSSSource.Description = this.textBox2.Text;

            // Obtiene los items del datagrid y los asigna a la fuente
            List<RSSItemDTO> list = new List<RSSItemDTO>();
            foreach (DataGridViewRow item in this.rSSItemsGridView.Rows)
            {
                list.Add((RSSItemDTO)item.DataBoundItem);
            }
            this.RSSSource.RSSItems = list;
        }

        private void rSSItemsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void bwRSSReader_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
